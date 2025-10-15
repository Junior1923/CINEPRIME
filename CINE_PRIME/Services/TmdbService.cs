using CINE_PRIME.Models.Tmdb;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace CINE_PRIME.Services
{
    public class TmdbService : ITmdbService
    {

        private readonly HttpClient _httpClient;
        private readonly TmdbSettings _settings;

        public TmdbService()
        {
            
        }
        public TmdbService(HttpClient httpClient, IOptions<TmdbSettings> settings) //IOptions para poder acceder a los datos en appsettings.json
        {
            _httpClient = httpClient;
            _settings = settings.Value; // .Value obtiene el objeto TmdbSettings con los valores del appsettings

        }


        #region GET PELICULAS POPULARES
        public async Task<List<TmdbMovieDTO>> GetPopularMoviesAsync()
        {
            try
            {
                #region URL API
                //1.Construye la URL para la petición a la API de TMDB
                // _settings.BaseUrl: URL base de la API
                // movie / popular: Endpoint para obtener películas populares
                // api_key: Tu clave de API para autenticación
                // language = es - ES: Resultados en español
                // page = 1: Primera página de resultados(TMDB pagina sus respuestas)

                var url = $"{_settings.BaseUrl}movie/popular?api_key={_settings.ApiKey}&language=es-ES&page=10";
                #endregion

                #region PETICION API
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonSerializer.Deserialize<TmdbResponseDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return data?.Results ?? new List<TmdbMovieDTO>(); // Retorna la lista de películas o una lista vacía si no hay resultados
                #endregion

            }
            catch (HttpRequestException ex)
            {
                // Manejar error de red/HTTP
                return new List<TmdbMovieDTO>(); // Retornar lista vacía en caso de error
            }
            catch (JsonException ex)
            {
                // Manejar error de deserialización
                return new List<TmdbMovieDTO>();
            }

        }
        #endregion


        #region GET DETALLE PELICULA
        public async Task<TmdbMovieDTO> GetMovieDetailsAsync(int movieId)
        {

            try
            {
                var url = $"{_settings.BaseUrl}movie/{movieId}?api_key={_settings.ApiKey}&language=es-ES";

                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var movie = JsonSerializer.Deserialize<TmdbMovieDTO>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return movie;

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


        #region GET TRAILER PELICULA
        public async Task<string?> GetMovieTrailerAsync(int movieId)
        {
            try
            {
                var url = $"{_settings.BaseUrl}movie/{movieId}/videos?api_key={_settings.ApiKey}&language=es-ES";

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



    }
}
