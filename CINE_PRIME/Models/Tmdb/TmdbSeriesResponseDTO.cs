using System.Text.Json.Serialization;

namespace CINE_PRIME.Models.Tmdb
{
    public class TmdbSeriesResponseDTO
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("results")]
        public List<TmdbSeriesDTO>? Results { get; set; }
    }
}
