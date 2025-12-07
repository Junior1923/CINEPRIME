using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class FavoriteItemVM
    {
        public Guid FavoriteId { get; set; }
        public int MediaId { get; set; }
        public string MediaType { get; set; }  // "movie" | "tv"

        // DTOs que pueden o no venir llenos
        public TmdbMovieDTO? Movie { get; set; }
        public TmdbSeriesDTO? Series { get; set; }

        // Propiedades útiles para no repetir lógica en la vista
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
    }
}
