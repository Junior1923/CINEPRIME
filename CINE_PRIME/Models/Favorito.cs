using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Favorito
{
    public Guid Id { get; set; }
    public string UserId { get; set; }     // FK -> AspNetUsers.Id
    public int MediaId { get; set; }           // MovieId o SeriesId proveniente de TMDB
    public string MediaType { get; set; }    
    public DateTime FechaAgregado { get; set; } = DateTime.Now;

    // propiedad de navegación
    public virtual ApplicationUser Usuario { get; set; }
}
