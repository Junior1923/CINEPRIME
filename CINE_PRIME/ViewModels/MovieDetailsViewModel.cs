using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class MovieDetailsViewModel
    {
        public TmdbMovieDTO Movie { get; set; } = null!;
        public string? TrailerKey { get; set; }

    }
}
