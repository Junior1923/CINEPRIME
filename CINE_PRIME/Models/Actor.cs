using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Actor
{
    public int ActorId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Biografia { get; set; }

    public string? UrlFoto { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public virtual ICollection<PeliculasActore> PeliculasActores { get; set; } = new List<PeliculasActore>();
}
