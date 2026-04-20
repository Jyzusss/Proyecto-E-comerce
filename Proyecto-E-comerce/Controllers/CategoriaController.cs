using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto_E_comerce.Dto.CategoriaDto;
using Proyecto_E_comerce.Models;

namespace Proyecto_E_comerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly EComerceContext _context;
        public CategoriaController(EComerceContext context)
        {
            _context = context;
        }

        [HttpGet("obtener")]
        public async Task<ActionResult<IEnumerable<CategoriaReadDto>>> GetCategorias()
        {
            var categorias = await _context.Categoria.ToListAsync();
            var categoriaReadDtos = categorias.Select(c => new CategoriaReadDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            }).ToList();
            return Ok(categoriaReadDtos);
        }
        [HttpGet("obtener/{id}")]

        public async Task<ActionResult<CategoriaReadDto>> GetCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }
            var categoriaReadDto = new CategoriaReadDto
            {
                Id = categoria.Id,
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion
            };
            return Ok(categoriaReadDto);
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<CategoriaReadDto>> CrearCategoria(CategoriaCreateDto categoriaCreateDto)
        {
            var categoria = new Categorium
            {
                Nombre = categoriaCreateDto.Nombre,
                Descripcion = categoriaCreateDto.Descripcion
            };

            try
            {
                _context.Categoria.Add(categoria);
                await _context.SaveChangesAsync();

                var categoriaReadDto = new CategoriaReadDto
                {
                    Id = categoria.Id,
                    Nombre = categoria.Nombre,
                    Descripcion = categoria.Descripcion
                };
                return CreatedAtAction(nameof(GetCategoria), new { id = categoria.Id }, categoriaReadDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al crear la categoría: {ex.Message}");
            }
        }
        [HttpPut("actualizar/{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, CategoriaCreateDto dto)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return NotFound();

            categoria.Nombre = dto.Nombre;
            categoria.Descripcion = dto.Descripcion;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null) return NotFound();
            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
