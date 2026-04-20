using Proyecto_E_comerce.Dto.OrdenItemDto;

namespace Proyecto_E_comerce.Dto.OrdenDto
{
    public class OrdenReadDto
    {
        public int Id { get; set; }
        public DateTime? OrdenDato { get; set; }
        public decimal Total { get; set; }
        public string Email { get; set; } = null!;
        public List<OrdenItemReadDto> Items { get; set; } = new();
    }
}
