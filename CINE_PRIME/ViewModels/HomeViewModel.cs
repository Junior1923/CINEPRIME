using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class HomeViewModel
    {
        public List<TmdbMovieDTO> PopularMovies { get; set; }
        public List<TmdbSeriesDTO> PopularSeries { get; set; }
    }
}
