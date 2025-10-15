using CINE_PRIME.Models;

namespace CINE_PRIME.Interfaces
{
    public interface IFavoritoService
    {
        Task<bool> AddFavoriteAsync(int movieId, string userId);
        Task<bool> RemoveFavoriteAsync(Guid favoritoId);
        Task<List<Favorito>> GetFavoritesByUserAsync(string userId);

    }
}
