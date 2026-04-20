using Proyecto_E_comerce.Dto.ordenItemDto;

namespace Proyecto_E_comerce.Dto.ordenDto
{
    public class OrdenCreateDto
    {
        public string Email { get; set; } = null!;
        public List<OrdenItemCreateDto> OrdenItems { get; set; } = new List<OrdenItemCreateDto>();
    }
}
