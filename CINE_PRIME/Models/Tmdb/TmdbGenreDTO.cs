using System.Text.Json.Serialization;

namespace CINE_PRIME.Models.Tmdb
{
    public class TmdbGenreDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

    }
}
