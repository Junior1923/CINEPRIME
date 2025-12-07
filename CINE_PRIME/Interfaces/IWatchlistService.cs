using CINE_PRIME.Models;

namespace CINE_PRIME.Interfaces
{
    public interface IWatchlistService
    {
        Task<bool> AddToWatchlistAsync(int mediaId, string mediaType, string userId);
        Task<bool> RemoveFromWatchlistAsync(Guid id);
        Task<List<ListaPendiente>> GetWatchlistByUserAsync(string userId);

    }

}
