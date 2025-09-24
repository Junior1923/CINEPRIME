using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Pago
{
    public int PagoId { get; set; }

    public int SuscripcionId { get; set; }

    public decimal Monto { get; set; }

    public string Moneda { get; set; } = null!;

    public string Proveedor { get; set; } = null!;

    public string? ReferenciaProveedor { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaPago { get; set; }

    public virtual Suscripcione Suscripcion { get; set; } = null!;
}
