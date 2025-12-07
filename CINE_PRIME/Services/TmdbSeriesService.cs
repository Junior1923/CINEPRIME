using CINE_PRIME.Interfaces;
using CINE_PRIME.Models.Tmdb;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CINE_PRIME.Services
{
    public class TmdbSeriesService : ITmdbSeriesService
    {

        private readonly HttpClient _httpClient;
        private readonly TmdbSettings _settings;

        public TmdbSeriesService()
        {
            
        }

        public TmdbSeriesService( HttpClient httpClient, IOptions<TmdbSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;

        }


        #region GET ON THE AIR SERIES
        public async Task<IEnumerable<TmdbSeriesDTO>> GetOnTheAirSeriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<TmdbSeriesResponseDTO>($"{_settings.BaseUrl}tv/on_the_air?api_key={_settings.ApiKey}&language=es-ES");
                return response?.Results ?? new List<TmdbSeriesDTO>();
            }
            catch (HttpRequestException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
            catch (JsonException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
        }
        #endregion


        #region GET POPULAR SERIES
        public async Task<List<TmdbSeriesDTO>> GetPopularSeriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<TmdbSeriesResponseDTO>($"{_settings.BaseUrl}tv/popular?api_key={_settings.ApiKey}&language=es-ES");
                return response?.Results ?? new List<TmdbSeriesDTO>();
            }
            catch (HttpRequestException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
            catch (JsonException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
        }
        #endregion


        #region GET SERIES DETAILS
        public async Task<TmdbSeriesDTO> GetSeriesDetailsAsync(int serieId)
        {
            try
            {
                var serie = await _httpClient.GetFromJsonAsync<TmdbSeriesDTO>($"{_settings.BaseUrl}tv/{serieId}?api_key={_settings.ApiKey}&language=es-ES");

                return serie;
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (JsonException ex)
            {
                return null;
            }
        }
        #endregion


        #region GET SERIES TRAILER
        public async Task<string?> GetSeriesTrailerAsync(int serieId)
        {
            try
            {
                var url = $"{_settings.BaseUrl}tv/{serieId}/videos?api_key={_settings.ApiKey}&language=es-ES";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(json);
                var results = doc.RootElement.GetProperty("results");

                foreach (var item in results.EnumerateArray())
                {
                    if (item.GetProperty("site").GetString() == "YouTube" &&
                        item.GetProperty("type").GetString() == "Trailer")
                    {
                        var key = item.GetProperty("key").GetString();
                        return $"https://www.youtube.com/embed/{key}";
                    }

                }

                return null; // No encontró tráiler disponible
            }
            catch (HttpRequestException ex)
            {
                return null;
            }
            catch (JsonException ex)
            {
                return null;
            }
        }
        #endregion


        #region GET TOP RATED SERIES
        public async Task<IEnumerable<TmdbSeriesDTO>> GetTopRatedSeriesAsync()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<TmdbSeriesResponseDTO>($"{_settings.BaseUrl}tv/top_rated?api_key={_settings.ApiKey}&language=es-ES");
                return response?.Results ?? new List<TmdbSeriesDTO>();
            }
            catch (HttpRequestException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
            catch (JsonException ex)
            {
                return new List<TmdbSeriesDTO>();
            }
        }
        #endregion

    }
}
