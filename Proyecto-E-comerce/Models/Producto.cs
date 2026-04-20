using System;
using System.Collections.Generic;

namespace Proyecto_E_comerce.Models;

public partial class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    public int CategoriaId { get; set; }

    public DateTime? Tiempo { get; set; }

    public virtual Categorium Categoria { get; set; } = null!;

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();
}
