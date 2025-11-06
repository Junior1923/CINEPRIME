using CINE_PRIME.Interfaces;
using CINE_PRIME.Models.Tmdb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class WatchlistController : Controller
    {

        private readonly IWatchlistService _watchlistService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITmdbService _tmdbService;

        public WatchlistController(IWatchlistService watchlistService, IHttpContextAccessor httpContextAccessor, ITmdbService tmdbService)
        {
            _watchlistService = watchlistService;
            _httpContextAccessor = httpContextAccessor;
            _tmdbService = tmdbService;
        }


        [HttpPost]
        public async Task<IActionResult> Add(int movieId)
        {
            // Obtener el ID del usuario autenticado
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var result = await _watchlistService.AddToWatchlistAsync(movieId, userId);

            if (!result)
            { 
                return BadRequest(); 
            }

            return RedirectToAction("Index", "Watchlist");

        }


        [HttpPost]
        public async Task<IActionResult> Remove(Guid listaId)
        {
            var result = await _watchlistService.RemoveFromWatchlistAsync(listaId);

            if (!result)
            { 
                return NotFound(); 
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Index()
        {
            // Obtener el ID del usuario autenticado
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            { 
               return BadRequest(); 
            }

            var listaPendientes = await _watchlistService.GetWatchlistByUserAsync(userId);

            var peliculasPendientes = new List<(Guid listaId, TmdbMovieDTO Movie)>();

            foreach (var p in listaPendientes)
            {
                var pelicula = await _tmdbService.GetMovieDetailsAsync(p.TmdbMovieId);

                if (pelicula != null)    
                { 
                    peliculasPendientes.Add((p.Id, pelicula)); 
                }
            }

            return View(peliculasPendientes);

        }

    }


}
