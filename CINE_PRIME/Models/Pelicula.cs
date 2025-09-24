using System;
using System.Collections.Generic;

namespace CINE_PRIME.Models;

public partial class Pelicula
{
    public int PeliculaId { get; set; }

    public string Titulo { get; set; } = null!;

    public string? Sinopsis { get; set; }

    public int? Anio { get; set; }

    public int? DuracionMin { get; set; }

    public string? UrlPoster { get; set; }

    public decimal? PromedioCalificacion { get; set; }

    public bool EsPremium { get; set; }

    public virtual ICollection<Favorito> Favoritos { get; set; } = new List<Favorito>();

    public virtual ICollection<ListaPendiente> ListaPendientes { get; set; } = new List<ListaPendiente>();

    public virtual ICollection<PeliculasActore> PeliculasActores { get; set; } = new List<PeliculasActore>();

    public virtual ICollection<Trailer> Trailers { get; set; } = new List<Trailer>();

    public virtual ICollection<Genero> Generos { get; set; } = new List<Genero>();
}
