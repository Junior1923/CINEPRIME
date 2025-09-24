using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Trailer
{
    public int TrailerId { get; set; }

    public int PeliculaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string UrlVideo { get; set; } = null!;

    public int? DuracionSeg { get; set; }

    public DateTime FechaPublicacion { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual Pelicula Pelicula { get; set; } = null!;
}
