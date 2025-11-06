using System.Diagnostics;
using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using Microsoft.AspNetCore.Mvc;

namespace CINE_PRIME.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITmdbService _tmdbService;



        public HomeController(ILogger<HomeController> logger, ITmdbService tmdbService)
        {
            _logger = logger;
            _tmdbService = tmdbService;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["FullWidth"] = true;
            var populares = await _tmdbService.GetPopularMoviesAsync();
            return View(populares.Take(12)); // solo mostramos 10 en el home
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
