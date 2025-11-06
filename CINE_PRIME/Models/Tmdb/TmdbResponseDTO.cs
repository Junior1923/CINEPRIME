using System.Text.Json.Serialization;

namespace CINE_PRIME.Models.Tmdb
{
    public class TmdbResponseDTO
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<TmdbMovieDTO> Results { get; set; }
    }
}
