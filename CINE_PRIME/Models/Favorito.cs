using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Favorito
{
    public int FavoritoId { get; set; }

    public string UsuarioId { get; set; } = null!;

    public int PeliculaId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Pelicula Pelicula { get; set; } = null!;

    public virtual Usuario Usuario { get; set; } = null!;
}
