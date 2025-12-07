using CINE_PRIME.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CINE_PRIME.ViewComponents
{
    public class UserNameViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserNameViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
            {
                return Content("Usuario");

            }
                
            var nombre = $"{user.Nombre}".Trim();

            // Si está vacío, retorna solo "Usuario"
            return Content(string.IsNullOrWhiteSpace(nombre)
                ? "Usuario"
                : nombre);
        }

    }

}
