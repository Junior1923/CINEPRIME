using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class BitacoraAuditorium
{
    public int LogId { get; set; }

    public string? UsuarioId { get; set; }

    public string Accion { get; set; } = null!;

    public string Entidad { get; set; } = null!;

    public string EntidadId { get; set; } = null!;

    public DateTime FechaCreacion { get; set; }

    public string? Ip { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
