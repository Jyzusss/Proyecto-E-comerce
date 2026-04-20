using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_E_comerce.Dto.ordenDto;
using Proyecto_E_comerce.Dto.OrdenDto;
using Proyecto_E_comerce.Dto.OrdenItemDto;
using Proyecto_E_comerce.Models;

namespace Proyecto_E_comerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly EComerceContext _context;

        public OrdenController(EComerceContext context)
        {
            _context = context;
        }
        [HttpGet("ObtenerOrdenes")]

        public async Task<ActionResult<IEnumerable<OrdenReadDto>>> GetOrdens()
        {
            var ordenes = await _context.Ordens
                .Include(o => o.OrdenItems)
                .ThenInclude(oi => oi.Producto)
                .OrderByDescending(o => o.OrdenDato)
                .Select(o => new OrdenReadDto
                {
                    Id = o.Id,
                    Email = o.Email,
                    OrdenDato = o.OrdenDato,
                    Total = o.Total,
                    Items = o.OrdenItems.Select(oi => new OrdenItemReadDto
                    {

                        ProductoNombre = oi.Producto.Nombre,
                        Cantidad = oi.Cantidad,
                        PrecioUnitario = oi.PrecioUnitario
                    }).ToList()
                }).ToListAsync();

            return Ok(ordenes);
        }
        [HttpGet("historial/{email}")]
        public async Task<ActionResult<IEnumerable<OrdenReadDto>>> GetHistorial(string email)
        {
            var ordenes = await _context.Ordens
                .Where(o => o.Email == email)
                .Include(o => o.OrdenItems)
                    .ThenInclude(oi => oi.Producto)
                .Select(o => new OrdenReadDto
                {
                    Id = o.Id,
                    OrdenDato = o.OrdenDato,
                    Total = o.Total,
                    Email = o.Email,
                    Items = o.OrdenItems.Select(oi => new OrdenItemReadDto
                    {
                        ProductoNombre = oi.Producto.Nombre,
                        Cantidad = oi.Cantidad,
                        PrecioUnitario = oi.PrecioUnitario
                    }).ToList()
                }).ToListAsync();

            return Ok(ordenes);
        }
        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<OrdenReadDto>> GetOrdenById(int id)
        {
            var orden = await _context.Ordens
                .Include(o => o.OrdenItems)
                    .ThenInclude(oi => oi.Producto)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (orden == null) return NotFound("La orden no existe.");

            var dto = new OrdenReadDto
            {
                Id = orden.Id,
                OrdenDato = orden.OrdenDato,
                Total = orden.Total,
                Email = orden.Email,
                Items = orden.OrdenItems.Select(oi => new OrdenItemReadDto
                {
                    ProductoNombre = oi.Producto.Nombre,
                    Cantidad = oi.Cantidad,
                    PrecioUnitario = oi.PrecioUnitario
                }).ToList()
            };

            return Ok(dto);
        }
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarOrden(int id)
        {
            var orden = await _context.Ordens.Include(o => o.OrdenItems).FirstOrDefaultAsync(o => o.Id == id);
            if (orden == null) return NotFound();
            _context.Ordens.Remove(orden);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("Crear")]

        public async Task<ActionResult> PostOrden(OrdenCreateDto ordenCreateDto)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                decimal totalOrden = 0;
                var nuevaOrden = new Orden
                {
                    Email = ordenCreateDto.Email,
                    OrdenDato = DateTime.UtcNow,
                    Total = 0
                };

                _context.Ordens.Add(nuevaOrden);
                await _context.SaveChangesAsync();
               
                foreach(var item in ordenCreateDto.OrdenItems)
                {
                    var producto  = await _context.Productos.FindAsync(item.ProductoId);
                    if(producto == null) return BadRequest($"Producto con ID {item.ProductoId} no encontrado.");
                    if(producto.Stock < item.Cantidad) return BadRequest($"Stock insuficiente para el producto {producto.Nombre}.");

                    producto.Stock -= item.Cantidad;

                    var precioVenta = producto.Precio;

                    totalOrden += precioVenta * item.Cantidad;

                    var detalle = new OrdenItem
                    {
                        OrdenId = nuevaOrden.Id,
                        ProductoId = item.ProductoId,
                        Cantidad = item.Cantidad,
                        PrecioUnitario= precioVenta
                    };
                    _context.OrdenItems.Add(detalle);
                }

                nuevaOrden.Total = totalOrden;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new
                {
                    mensaje = "Orden procesada con éxito",
                    id = nuevaOrden.Id,
                    total = totalOrden
                });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, $"Error interno: {ex.Message}");
            }

        }
        }
    }

