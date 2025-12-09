using CINE_PRIME.Models;
using CINE_PRIME.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CINE_PRIME.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        #region REGISTER
        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                Nombre = model.Nombre,
                Apellido = model.Apellido,
                UserName = model.Email,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FechaCreacion = DateTime.Now
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                ViewBag.SuccessMessage = "✅ Tu cuenta ha sido creada correctamente. Ahora puedes iniciar sesión.";
                ModelState.Clear(); // limpia el formulario
                return View(); // se queda en la misma página de registro

            }

            foreach (var error in result.Errors)
            {
                Console.WriteLine($"{error.Description}");
                ModelState.AddModelError("", error.Description);

            }

            return View(model);
        }
        #endregion


        #region LOGIN - LOGOUT
        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {

            if (!ModelState.IsValid)
                return View(model);

            // Buscar usuario
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }

            // Validar password
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Credenciales inválidas");
                return View(model);
            }

            // Crear claims personalizados
            var claims = new List<Claim>
            {
            new Claim("Nombre", user.Nombre),
            new Claim("Apellido", user.Apellido)
            };

            // Hacer login generando cookie con claims
            await _signInManager.SignInWithClaimsAsync(user, model.RememberMe, claims);

            return RedirectToAction("Index", "Movies");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion


    }
}
