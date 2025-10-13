using CINE_PRIME.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class PeliculasController : Controller
    {

        private readonly ITmdbService _tmdbService;

        public PeliculasController(ITmdbService tmdbService)
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
        public async Task<IActionResult> Detalle(int id)
        {
            var pelicula = await _tmdbService.GetMovieDetailsAsync(id);
            var trailer = await _tmdbService.GetMovieTrailerAsync(id);

            if (pelicula == null)
            {
                return NotFound();
            }

            ViewBag.trailer = trailer;

            return View(pelicula);
        }


    }


}
