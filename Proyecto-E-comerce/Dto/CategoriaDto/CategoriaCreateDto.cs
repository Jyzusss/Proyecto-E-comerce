using System.ComponentModel.DataAnnotations;

namespace Proyecto_E_comerce.Dto.CategoriaDto
{
    public class CategoriaCreateDto
    {
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio.")]
        [StringLength(100)]
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
