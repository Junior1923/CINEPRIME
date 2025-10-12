using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Usuario
{
    public string UsuarioId { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string ContrasenaHash { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public virtual ICollection<BitacoraAuditoria> BitacoraAuditoria { get; set; } = new List<BitacoraAuditoria>();

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<ListaPendiente> ListaPendientes { get; set; } = new List<ListaPendiente>();

    public virtual PerfilUsuario? PerfilesUsuario { get; set; }

    public virtual ICollection<Suscripcion> Suscripciones { get; set; } = new List<Suscripcion>();
}
