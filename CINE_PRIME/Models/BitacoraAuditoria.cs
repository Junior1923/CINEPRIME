using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CINE_PRIME.Models;

public partial class BitacoraAuditoria
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }    
    public string Accion { get; set; }
    public DateTime FechaAccion { get; set; } = DateTime.Now;
    public string? Detalle { get; set; }

    public virtual ApplicationUser Usuario { get; set; }
}
