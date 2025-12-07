using System.Text.Json.Serialization;

namespace CINE_PRIME.Models.Tmdb
{
    public class TmdbSeriesDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("overview")]
        public string? Overview { get; set; }

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("backdrop_path")]
        public string? BackdropPath { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("first_air_date")]
        public string? FirstAirDate { get; set; }


        //Agregados
        [JsonPropertyName("number_of_seasons")]
        public int NumberOfSeasons { get; set; }

        [JsonPropertyName("number_of_episodes")]
        public int NumberOfEpisodes { get; set; }

        [JsonPropertyName("episode_run_time")]
        public List<int>? EpisodeRunTime { get; set; }

        [JsonPropertyName("genres")]
        public List<TmdbGenreDTO>? Genres { get; set; }


    }

}
