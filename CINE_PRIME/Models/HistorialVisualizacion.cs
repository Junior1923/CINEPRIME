namespace CINE_PRIME.Models
{
    public partial class HistorialVisualizacion
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }     // FK -> AspNetUsers.Id
        public int TmdbMovieId { get; set; }
        public DateTime FechaVisualizacion { get; set; } = DateTime.Now;

        public virtual ApplicationUser Usuario { get; set; }

    }
}
