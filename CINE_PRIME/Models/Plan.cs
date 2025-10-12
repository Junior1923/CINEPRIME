using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Plan
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public decimal PrecioMensual { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; } = true;
}
