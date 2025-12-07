using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class MoviesHomeViewModel
    {
        public IEnumerable<TmdbMovieDTO> Populares { get; set; } = new List<TmdbMovieDTO>();
        public IEnumerable<TmdbMovieDTO> TopRated { get; set; } = new List<TmdbMovieDTO>();
        public IEnumerable<TmdbMovieDTO> Upcoming { get; set; } = new List<TmdbMovieDTO>();
        public IEnumerable<TmdbMovieDTO> NowPlaying { get; set; } = new List<TmdbMovieDTO>();
    }
}
