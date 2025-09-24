using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Suscripcione
{
    public int SuscripcionId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public int PlanId { get; set; }

    public DateTime FechaInicio { get; set; }

    public DateTime? FechaFin { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();

    public virtual Plane Plan { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
