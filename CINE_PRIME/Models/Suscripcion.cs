using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Suscripcion
{
    public int Id { get; set; }
    public string UserId { get; set; }     // FK -> AspNetUsers.Id
    public int PlanId { get; set; }        // FK -> Plan.Id
    public DateTime FechaInicio { get; set; } = DateTime.Now;
    public DateTime? FechaFin { get; set; }
    public string? Estado { get; set; }    // Ej: Active, Cancelled, Pending

    // Propiedades de navegación
    public virtual ApplicationUser Usuario { get; set; }
    public virtual Plan Plan { get; set; }
}
