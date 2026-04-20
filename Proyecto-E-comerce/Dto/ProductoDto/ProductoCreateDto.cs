using System.ComponentModel.DataAnnotations;

namespace Proyecto_E_comerce.Dto.ProductoDto
{
    public class ProductoCreateDto
    {
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(200)]
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaId { get; set; }
    }
}
