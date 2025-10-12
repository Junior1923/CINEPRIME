using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class PerfilUsuario
{
    public Guid Id { get; set; }
    public string UserId { get; set; }   // FK -> AspNetUsers.Id
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? FotoPerfil { get; set; }
    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    // Navegación
    public virtual ApplicationUser Usuario { get; set; }
}
