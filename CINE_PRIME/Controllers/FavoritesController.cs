using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using CINE_PRIME.Models.Tmdb;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoriteService _favoritoService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITmdbService _tmdbService;
        private readonly ITmdbSeriesService _tmdbSeriesService;

        public FavoritesController(IFavoriteService favoritoService, IHttpContextAccessor httpContextAccessor, ITmdbService tmdbService, ITmdbSeriesService tmdbSeriesService)
        {
            _favoritoService = favoritoService;
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
                return Unauthorized();
            }

            // Llamar al servicio para agregar el favorito
            var result = await _favoritoService.AddFavoriteAsync(mediaId, mediaType, userId);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public async Task<IActionResult> Remove(Guid favoritoId)
        {
            // Llamar al servicio para eliminar el favorito
            var result = await _favoritoService.RemoveFavoriteAsync(favoritoId);

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

            // Llamar al servicio para obtener los favoritos del usuario
            var favoritos = await _favoritoService.GetFavoritesByUserAsync(userId);


            // Crear una lista para almacenar los detalles de las películas y series favoritas
            var favMix = new List<FavoriteItemVM>();

            foreach (var fav in favoritos)
            {
                var vm = new FavoriteItemVM
                {
                    FavoriteId = fav.Id,
                    MediaId = fav.MediaId,
                    MediaType = fav.MediaType
                };

                if (fav.MediaType == "movie")
                {
                    vm.Movie = await _tmdbService.GetMovieDetailsAsync(fav.MediaId);
                }
                else if (fav.MediaType == "tv")
                {
                    vm.Series = await _tmdbSeriesService.GetSeriesDetailsAsync(fav.MediaId);
                }

                favMix.Add(vm);
            }

            return View(favMix);

        }

    }
}
