using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.Interfaces
{
    public interface ITmdbSeriesService
    {
        Task<List<TmdbSeriesDTO>> GetPopularSeriesAsync();
        Task<IEnumerable<TmdbSeriesDTO>> GetTopRatedSeriesAsync();
        Task<IEnumerable<TmdbSeriesDTO>> GetOnTheAirSeriesAsync();
        Task<TmdbSeriesDTO> GetSeriesDetailsAsync(int id);
        Task<string?> GetSeriesTrailerAsync(int serieId);
    }

}
