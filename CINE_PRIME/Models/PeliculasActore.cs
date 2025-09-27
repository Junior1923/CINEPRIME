using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class PeliculasActore
{
    public int PeliculaId { get; set; }

    public int ActorId { get; set; }

    public string? NombrePersonaje { get; set; }

    public virtual Actor Actor { get; set; } = null!;

    public virtual Pelicula Pelicula { get; set; } = null!;
}
