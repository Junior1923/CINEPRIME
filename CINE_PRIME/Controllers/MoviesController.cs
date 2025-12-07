using CINE_PRIME.Interfaces;
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
            //var peliculas = await _tmdbService.GetPopularMoviesAsync();
            
            //if (peliculas == null)
            //{
            //    return NotFound();
            //}
            //return View(peliculas);

            var populares = await _tmdbService.GetPopularMoviesAsync();
            var topRated = await _tmdbService.GetTopRatedMoviesAsync();
            var upcoming = await _tmdbService.GetUpcomingMoviesAsync();
            var nowPlaying = await _tmdbService.GetNowPlayingMoviesAsync();

            var model = new MoviesHomeViewModel
            {
                Populares = populares,
                TopRated = topRated,
                Upcoming = upcoming,
                NowPlaying = nowPlaying
            };

            return View(model);

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
