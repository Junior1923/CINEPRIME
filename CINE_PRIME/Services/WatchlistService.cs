using CINE_PRIME.Data;
using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Services
{
    public class WatchlistService : IWatchlistService
    {

        private readonly CinePrimeContext _context;

        public WatchlistService(CinePrimeContext context)
        {
            _context = context;
        }

        public async Task<bool> AddToWatchlistAsync(int mediaId, string mediaType, string userId)
        {
            try
            {
                var exists = await _context.ListasPendientes
                    .AnyAsync(w => w.MediaId == mediaId && w.UserId == userId);

                if (exists)
                { 
                    return false; 
                }

                var item = new ListaPendiente
                {
                    MediaId = mediaId,
                    MediaType = mediaType,
                    UserId = userId,
                    FechaAgregado = DateTime.Now
                };

                _context.ListasPendientes.Add(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }

        public async Task<List<ListaPendiente>> GetWatchlistByUserAsync(string userId)
        {
            try
            {
                var lista = await _context.ListasPendientes
                    .Where(w => w.UserId == userId)
                    .OrderByDescending(w => w.FechaAgregado)
                    .ToListAsync();

                return lista;
            }
            catch (Exception)
            {
                return new List<ListaPendiente>(); // Retorna una lista vacía en caso de error
            }

        }

        public async Task<bool> RemoveFromWatchlistAsync(Guid id)
        {
            try
            {
                var item = await _context.ListasPendientes.FirstOrDefaultAsync(l => l.Id == id);

                if (item == null)
                {
                    return false;
                }

                _context.ListasPendientes.Remove(item);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

    }
}
