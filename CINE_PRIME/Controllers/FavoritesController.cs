using CINE_PRIME.Interfaces;
using CINE_PRIME.Models.Tmdb;
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

        public FavoritesController(IFavoriteService favoritoService, IHttpContextAccessor httpContextAccessor, ITmdbService tmdbService)
        {
            _favoritoService = favoritoService;
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

            // Llamar al servicio para agregar el favorito
            var result = await _favoritoService.AddFavoriteAsync(movieId, userId);

            if (!result)
            {
                return BadRequest();
            }

            return RedirectToAction("Index", "Favorites");

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

            
            // Crear una lista para almacenar los detalles de las películas favoritas
            var peliculasFavoritas = new List<(Guid FavId, TmdbMovieDTO Movie)>();

            foreach (var f in favoritos)
            {
                var pelicula = await _tmdbService.GetMovieDetailsAsync(f.TmdbMovieId);

                peliculasFavoritas.Add((f.Id, pelicula));

            }

            return View(peliculasFavoritas);

        }

    }
}
