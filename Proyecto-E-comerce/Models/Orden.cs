using System;
using System.Collections.Generic;

namespace Proyecto_E_comerce.Models;

public partial class Orden
{
    public int Id { get; set; }

    public DateTime? OrdenDato { get; set; }

    public decimal Total { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<OrdenItem> OrdenItems { get; set; } = new List<OrdenItem>();
}
