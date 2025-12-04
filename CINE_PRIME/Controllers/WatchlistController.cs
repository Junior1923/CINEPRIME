using CINE_PRIME.Interfaces;
using CINE_PRIME.Models.Tmdb;
using CINE_PRIME.Services;
using CINE_PRIME.ViewModels;
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
        private readonly ITmdbSeriesService _tmdbSeriesService;

        public WatchlistController(IWatchlistService watchlistService, IHttpContextAccessor httpContextAccessor, ITmdbService tmdbService, ITmdbSeriesService tmdbSeriesService)
        {
            _watchlistService = watchlistService;
            _httpContextAccessor = httpContextAccessor;
            _tmdbService = tmdbService;
            _tmdbSeriesService = tmdbSeriesService;
        }


        [HttpPost]
        public async Task<IActionResult> Add(int mediaId, string mediaType)
        {
            // Obtener el ID del usuario autenticado
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest();
            }

            var result = await _watchlistService.AddToWatchlistAsync(mediaId, mediaType, userId);

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
               return Unauthorized(); 
            }

            var watchlist = await _watchlistService.GetWatchlistByUserAsync(userId);

            var items = new List<WatchlistItemVM>();

            foreach (var item in watchlist)
            {
                var vm = new WatchlistItemVM
                {
                    WatchlistId = item.Id,
                    MediaId = item.MediaId,
                    MediaType = item.MediaType
                };

                if (item.MediaType == "movie")
                {
                    vm.Movie = await _tmdbService.GetMovieDetailsAsync(item.MediaId);
                }
                else if (item.MediaType == "tv")
                {
                    vm.Series = await _tmdbSeriesService.GetSeriesDetailsAsync(item.MediaId);
                }

                items.Add(vm);
            }

            return View(items);

        }

    }


}
