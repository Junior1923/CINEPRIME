using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Genero
{
    public int GeneroId { get; set; }
    public string Nombre { get; set; } = null!;


    public ICollection<PeliculasGenero> PeliculasGeneros { get; set; } = new List<PeliculasGenero>();
    
}
