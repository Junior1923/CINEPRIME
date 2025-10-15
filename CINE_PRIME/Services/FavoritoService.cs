using CINE_PRIME.Data;
using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Services
{
    public class FavoritoService : IFavoritoService
    {
        private readonly CinePrimeContext _context;

        public FavoritoService(CinePrimeContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFavoriteAsync(int movieId, string userId)
        {
            try
            {
                // Verificar si ya existe el favorito
                var existeFavorito = await _context.Favoritos
                    .AnyAsync(f => f.UserId == userId && f.TmdbMovieId == movieId);

                if (existeFavorito)
                {
                    return false;
                }

                // Crear el nuevo favorito
                var favorito = new Favorito
                {
                    UserId = userId,
                    TmdbMovieId = movieId,
                    FechaAgregado = DateTime.Now
                };

                _context.Favoritos.Add(favorito);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Favorito>> GetFavoritesByUserAsync(string userId)
        {

            var favoritos = await _context.Favoritos
                .Where(f => f.UserId == userId)
                .OrderByDescending(f => f.FechaAgregado)
                .ToListAsync();

            if (favoritos == null)
            {
                return new List<Favorito>();// Retorna una lista vacía si no hay favoritos
            }

            return favoritos;

        }

        public async Task<bool> RemoveFavoriteAsync(Guid favoritoId)
        {
            try
            {
                // Buscar el favorito por ID
                var favorito = await _context.Favoritos
                    .FirstOrDefaultAsync(f => f.Id == favoritoId);

                if (favorito == null)
                {
                    return false;
                }

                // Eliminar el favorito
                _context.Favoritos.Remove(favorito);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
