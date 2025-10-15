using CINE_PRIME.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class FavoritesController : Controller
    {
        private readonly IFavoritoService _favoritoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoritesController(IFavoritoService favoritoService, IHttpContextAccessor httpContextAccessor)
        {
            _favoritoService = favoritoService;
            _httpContextAccessor = httpContextAccessor;
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

            return View(favoritos);

        }

    }
}
