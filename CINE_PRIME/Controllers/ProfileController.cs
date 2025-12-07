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

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
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
