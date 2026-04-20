namespace Proyecto_E_comerce.Dto.ProductoDto
{
    public class ProductoReadDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
