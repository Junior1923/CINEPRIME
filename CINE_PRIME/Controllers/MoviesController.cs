using CINE_PRIME.Services;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {

        private readonly ITmdbService _tmdbService;

        public MoviesController(ITmdbService tmdbService)
        {
            _tmdbService = tmdbService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var peliculas = await _tmdbService.GetPopularMoviesAsync();
            
            if (peliculas == null)
            {
                return NotFound();
            }
            return View(peliculas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _tmdbService.GetMovieDetailsAsync(id);
            var trailer = await _tmdbService.GetMovieTrailerAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            var model = new MovieDetailsViewModel
            {
                Movie = movie,
                TrailerKey = trailer
            };

            ViewBag.trailer = trailer;

            return View(model);
        }


    }


}
