using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.Interfaces
{
    public interface ITmdbService
    {
        Task<List<TmdbMovieDTO>> GetPopularMoviesAsync();
        Task<TmdbMovieDTO> GetMovieDetailsAsync(int movieId);
        Task<string?> GetMovieTrailerAsync(int movieId);


    }
}
