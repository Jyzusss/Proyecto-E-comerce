namespace Proyecto_E_comerce.Dto.OrdenItemDto
{
    public class OrdenItemReadDto
    {
        public string ProductoNombre { get; set; } = null!;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
