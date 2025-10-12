namespace CINE_PRIME.Models
{
    public class PeliculaLocal
    {
        public int Id { get; set; }
        public int TmdbMovieId { get; set; }   // único por película
        public string? Titulo { get; set; }
        public string? ImagenUrl { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? FechaLanzamiento { get; set; }
        public DateTime FechaGuardado { get; set; } = DateTime.Now;

    }
}
