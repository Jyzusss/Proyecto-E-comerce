using System;
using System.Collections.Generic;

namespace Proyecto_E_comerce.Models;

public partial class OrdenItem
{
    public int Id { get; set; }

    public int OrdenId { get; set; }

    public int ProductoId { get; set; }

    public int Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public virtual Orden Orden { get; set; } = null!;

    public virtual Producto Producto { get; set; } = null!;
}
