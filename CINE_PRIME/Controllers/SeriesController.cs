using CINE_PRIME.Interfaces;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class SeriesController : Controller
    {

        private readonly ITmdbSeriesService _tmdbSeriesService;

        public SeriesController(ITmdbSeriesService tmdbSeriesService)
        {
            _tmdbSeriesService = tmdbSeriesService;
        }



        public async Task<IActionResult> Index()
        {
            var series = await _tmdbSeriesService.GetPopularSeriesAsync();
            var otras = await _tmdbSeriesService.GetTopRatedSeriesAsync();

            var todas = series.Concat(otras).ToList();

            if (series == null)
                return NotFound();

            return View(todas);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var serie = await _tmdbSeriesService.GetSeriesDetailsAsync(id);
            var trailer = await _tmdbSeriesService.GetSeriesTrailerAsync(id);

            if (serie == null)
                return NotFound();

            var model = new SeriesDetailsViewModel
            {
                Serie = serie,
                TrailerKey = trailer
            };

            return View(model);
        }

    }
}
