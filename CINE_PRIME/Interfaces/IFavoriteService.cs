using CINE_PRIME.Models;

namespace CINE_PRIME.Interfaces
{
    public interface IFavoriteService
    {
        Task<bool> AddFavoriteAsync(int mediaId, string mediaType, string userId);
        Task<bool> RemoveFavoriteAsync(Guid favoritoId);
        Task<List<Favorito>> GetFavoritesByUserAsync(string userId);

    }
}
