using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class ListaPendiente
{
    public int ListaId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public int PeliculaId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Pelicula Pelicula { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
