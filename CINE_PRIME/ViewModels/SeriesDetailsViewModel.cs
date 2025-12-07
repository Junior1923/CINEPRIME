using CINE_PRIME.Models.Tmdb;

namespace CINE_PRIME.ViewModels
{
    public class SeriesDetailsViewModel
    {

        public TmdbSeriesDTO Serie { get; set; } = null!;

        public string? TrailerKey { get; set; }

    }
}
