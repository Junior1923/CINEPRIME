using CINE_PRIME.Data;
using CINE_PRIME.Interfaces;
using CINE_PRIME.Models;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CINE_PRIME.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public ProfileController(IProfileService profileService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _profileService = profileService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var vm = await _profileService.GetProfileAsync(userId);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileVM model, IFormFile? profilePicture)
        {
            var result = await _profileService.UpdateProfileAsync(model, profilePicture);

            if (!result.Success)
            {
                TempData["ErrorProfile"] = result.ErrorMessage;
                return RedirectToAction("Index");
            }

           
            // REGENERAR CLAIMS SIN LOGOUT
       
            var user = await _userManager.GetUserAsync(User);

            // Limpiar claims antiguos
            var existingClaims = await _userManager.GetClaimsAsync(user);

            foreach (var claim in existingClaims.Where(c => c.Type == "Nombre" || c.Type == "Apellido"))
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // Agregar claims nuevos
            await _userManager.AddClaimAsync(user, new Claim("Nombre", user.Nombre));
            await _userManager.AddClaimAsync(user, new Claim("Apellido", user.Apellido));

            // Regenerar cookie sin cerrar sesión
            await _signInManager.RefreshSignInAsync(user);
            // ================================


            TempData["ProfileUpdated"] = "Perfil actualizado correctamente.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ProfileVM model)
        {
            var result = await _profileService.ChangePasswordAsync(model);

            if (!result.Success)
            {
                TempData["ErrorPassword"] = result.ErrorMessage;
                return RedirectToAction("Index");
            }

            TempData["ProfileUpdated"] = "Contraseña cambiada correctamente.";
            return RedirectToAction("Index");
        }



    }
}
