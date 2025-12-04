using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using CINE_PRIME.Services;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CINE_PRIME.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITmdbService _tmdbService;
        private readonly ITmdbSeriesService _tmdbSeriesService;

        public HomeController(ILogger<HomeController> logger, ITmdbService tmdbService, ITmdbSeriesService tmdbSeriesService)
        {
            _logger = logger;
            _tmdbService = tmdbService;
            _tmdbSeriesService = tmdbSeriesService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["FullWidth"] = true;
            var vm = new HomeViewModel
            {
                PopularMovies = (await _tmdbService.GetPopularMoviesAsync()).Take(6).ToList(),
                PopularSeries = (await _tmdbSeriesService.GetPopularSeriesAsync()).Take(6).ToList()
            };

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }


}
