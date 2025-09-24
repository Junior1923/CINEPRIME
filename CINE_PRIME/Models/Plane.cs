using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Plane
{
    public int PlanId { get; set; }

    public string Nombre { get; set; } = null!;

    public decimal Precio { get; set; }

    public int MesesPeriodo { get; set; }

    public int? MaxListaPendiente { get; set; }

    public string? CaracteristicasJson { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Suscripcione> Suscripciones { get; set; } = new List<Suscripcione>();
}
