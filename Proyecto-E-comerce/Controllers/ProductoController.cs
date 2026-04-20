using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Proyecto_E_comerce.Dto.ProductoDto;
using Proyecto_E_comerce.Models;

namespace Proyecto_E_comerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly EComerceContext _context;
        public ProductoController(EComerceContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoReadDto>>> GetProducto()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria).ToListAsync();

            var productosDto = productos.Select(p => new ProductoReadDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion,
                Precio = p.Precio,
                Stock = p.Stock,
                NombreCategoria = p.Categoria.Nombre
            }).ToList();
            return Ok(productosDto);
        }
        [HttpPost]

        public async Task<ActionResult<ProductoReadDto>> CrearProducto(ProductoCreateDto productoCreateDto)
        {
            var categoria = await _context.Categoria.FindAsync(productoCreateDto.CategoriaId);
            if (categoria == null)
            {
                return BadRequest("La categoría especificada no existe.");
            }
            var producto = new Producto
            {
                Nombre = productoCreateDto.Nombre,
                Descripcion = productoCreateDto.Descripcion,
                Precio = productoCreateDto.Precio,
                Stock = productoCreateDto.Stock,
                CategoriaId = productoCreateDto.CategoriaId,
                Tiempo = DateTime.Now
            };
            try
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                await _context.Entry(producto).Reference(p => p.Categoria).LoadAsync();

                var readDto = new ProductoReadDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    NombreCategoria = producto.Categoria.Nombre
                };

                return CreatedAtAction(nameof(GetProductoById), new { id = producto.Id }, readDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear el producto: {ex.Message}");

            }
            
        }
        [HttpGet("obtener/{id}")]
        public async Task<ActionResult<ProductoReadDto>> GetProductoById(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            return Ok(new ProductoReadDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                NombreCategoria = producto.Categoria.Nombre
            });
        }
        // Actualizar Producto
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, ProductoCreateDto productoDto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            // Validar si la nueva categoría existe
            var existeCat = await _context.Categoria.AnyAsync(c => c.Id == productoDto.CategoriaId);
            if (!existeCat) return BadRequest("Categoría no válida");

            producto.Nombre = productoDto.Nombre;
            producto.Descripcion = productoDto.Descripcion;
            producto.Precio = productoDto.Precio;
            producto.Stock = productoDto.Stock;
            producto.CategoriaId = productoDto.CategoriaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Eliminar Producto
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
