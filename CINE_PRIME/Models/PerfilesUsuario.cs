using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class PerfilesUsuario
{
    public string UsuarioId { get; set; } = null!;

    public string? NombreMostrar { get; set; }

    public string? UrlAvatar { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario Usuario { get; set; } = null!;
}
