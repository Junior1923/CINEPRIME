using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CINE_PRIME.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IFavoriteService _favoriteService;
        private readonly IWatchlistService _watchlistService;
        private readonly IWebHostEnvironment _env;

        public ProfileService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IFavoriteService favoriteService,
            IWatchlistService watchlistService,
            IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _favoriteService = favoriteService;
            _watchlistService = watchlistService;
            _env = env;
        }

        // ---------------------------------------------------------------------
        // GET PROFILE
        // ---------------------------------------------------------------------
        public async Task<ProfileVM?> GetProfileAsync(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return null;

            var fav = (await _favoriteService.GetFavoritesByUserAsync(userId)).Count;
            var pend = (await _watchlistService.GetWatchlistByUserAsync(userId)).Count;

            return new ProfileVM
            {
                UserId = user.Id,
                Nombre = user.Nombre,
                Apellido = user.Apellido,
                Correo = user.Email,
                ImagenPerfil = user.ImagenPerfil,
                FechaRegistro = user.FechaCreacion,

                CantFavoritos = fav,
                CantPendientes = pend,

                EditNombre = user.Nombre,
                EditApellido = user.Apellido,
                EditCorreo = user.Email
            };
        }

        // ---------------------------------------------------------------------
        // UPDATE PROFILE
        // ---------------------------------------------------------------------
        public async Task<(bool Success, string ErrorMessage)> UpdateProfileAsync(ProfileVM model, IFormFile? profilePicture)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return (false, "El usuario no existe.");

            bool modified = false;

            if (user.Nombre != model.EditNombre)
            {
                user.Nombre = model.EditNombre;
                modified = true;
            }

            if (user.Apellido != model.EditApellido)
            {
                user.Apellido = model.EditApellido;
                modified = true;
            }

            if (user.Email != model.EditCorreo)
            {
                user.Email = model.EditCorreo;
                user.UserName = model.EditCorreo;
                modified = true;
            }

            // --- Procesar imagen ---
            if (profilePicture != null && profilePicture.Length > 0)
            {
                var valid = await ProcessProfilePicture(user, profilePicture);
                if (!valid.Success)
                    return valid;

                modified = true;
            }

            if (!modified)
                return (false, "No hiciste ningún cambio.");

            var update = await _userManager.UpdateAsync(user);
            if (!update.Succeeded)
                return (false, string.Join(" | ", update.Errors.Select(e => e.Description)));

            await _signInManager.RefreshSignInAsync(user);

            return (true, "");
        }

        // ---------------------------------------------------------------------
        // CHANGE PASSWORD
        // ---------------------------------------------------------------------
        public async Task<(bool Success, string ErrorMessage)> ChangePasswordAsync(ProfileVM model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
                return (false, "Usuario no encontrado.");

            var result = await _userManager.ChangePasswordAsync(user, model.ContrasenaActual!, model.NuevaContrasena!);

            if (!result.Succeeded)
                return (false, string.Join(" | ", result.Errors.Select(e => e.Description)));

            await _signInManager.RefreshSignInAsync(user);
            return (true, "");
        }

        // ---------------------------------------------------------------------
        // FILE PROCESSING
        // ---------------------------------------------------------------------
        private async Task<(bool Success, string ErrorMessage)> ProcessProfilePicture(ApplicationUser user, IFormFile file)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var maxBytes = 2 * 1024 * 1024;
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
                return (false, "Formato de imagen no permitido.");

            if (file.Length > maxBytes)
                return (false, "La imagen es demasiado grande. Máx 2MB.");

            var folder = Path.Combine(_env.WebRootPath, "images", "profiles");
            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            var fileName = $"{Guid.NewGuid()}{ext}";
            var filePath = Path.Combine(folder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Borrar anterior
            if (!string.IsNullOrEmpty(user.ImagenPerfil))
            {
                try
                {
                    var old = user.ImagenPerfil.TrimStart('/');
                    var oldPath = Path.Combine(_env.WebRootPath, old);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
                catch { }
            }

            user.ImagenPerfil = $"/images/profiles/{fileName}";
            return (true, "");
        }
    }
}
