using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class WatchlistItemVM
    {
        public Guid WatchlistId { get; set; }
        public int MediaId { get; set; }
        public string MediaType { get; set; }  // "movie" | "tv"

        // DTOs que se llenarán dependiendo del tipo
        public TmdbMovieDTO? Movie { get; set; }
        public TmdbSeriesDTO? Series { get; set; }

        // ============================
        // PROPIEDADES CALCULADAS
        // ============================

        public string Title =>
            MediaType == "movie" ? Movie?.Title :
            MediaType == "tv" ? Series?.Name :
            "Título no disponible";

        public string Poster =>
            MediaType == "movie" ? Movie?.PosterPath :
            MediaType == "tv" ? Series?.PosterPath :
            null;

        public string Backdrop =>
            MediaType == "movie" ? Movie?.BackdropPath :
            MediaType == "tv" ? Series?.BackdropPath :
            null;

        public double Rating =>
            MediaType == "movie" ? (Movie?.VoteAverage ?? 0) :
            MediaType == "tv" ? (Series?.VoteAverage ?? 0) :
            0;

        public string ReleaseDate =>
            MediaType == "movie" ? Movie?.ReleaseDate :
            MediaType == "tv" ? Series?.FirstAirDate :
            "";

        public string MediaTypeLabel =>
            MediaType == "movie" ? "Película" : "Serie";
    }
}
