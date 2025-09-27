namespace CINE_PRIME.Models
{
    public class PeliculasGenero
    {
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; } = null!;

        public int GeneroId { get; set; }
        public Genero Genero { get; set; } = null!;

    }
}
