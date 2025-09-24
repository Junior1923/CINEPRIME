using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Usuario
{
    public string UsuarioId { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<BitacoraAuditorium> BitacoraAuditoria { get; set; } = new List<BitacoraAuditorium>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<ListaPendiente> ListaPendientes { get; set; } = new List<ListaPendiente>();

    public virtual PerfilesUsuario? PerfilesUsuario { get; set; }

    public virtual ICollection<Suscripcione> Suscripciones { get; set; } = new List<Suscripcione>();
}
