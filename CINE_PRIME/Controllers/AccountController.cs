using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CINE_PRIME.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using CINE_PRIME.Data;

namespace CINE_PRIME.Controllers
{
    public class AccountController : Controller
    {
        private readonly CinePrimeContext _context;

        public AccountController(CinePrimeContext context)
        {
            _context = context;
        }

        // ---------------------
        // REGISTER
        // ---------------------
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string nombre, string correo, string password)
        {
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View();
            }

            // Verificar si ya existe el correo
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("", "Este correo ya está registrado.");
                return View();
            }

            // Crear hash de contraseña
            var hash = CrearHash(password);

            // Crear usuario
            var nuevoUsuario = new Usuario
            {
                UsuarioId = nombre, //  Se guarda el nombre como ID 
                Correo = correo,
                ContrasenaHash = hash,
                FechaRegistro = DateTime.Now
            };

            _context.Usuarios.Add(nuevoUsuario);

            // Crear perfil con datos ingresados
            var perfil = new PerfilesUsuario
            {
                UsuarioId = nuevoUsuario.UsuarioId,
                NombreMostrar = nombre,
                UrlAvatar = null,
                FechaCreacion = DateTime.Now
            };

            _context.PerfilesUsuarios.Add(perfil);
            await _context.SaveChangesAsync();

            // Guardar en sesión
            HttpContext.Session.SetString("UsuarioId", nuevoUsuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioCorreo", nuevoUsuario.Correo);
            HttpContext.Session.SetString("UsuarioNombre", perfil.NombreMostrar ?? nuevoUsuario.Correo);

            // Autenticación con cookies
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, nuevoUsuario.UsuarioId),
                new Claim(ClaimTypes.Email, nuevoUsuario.Correo)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme))
            );

            return RedirectToAction("Index", "Home");
        }

        // ---------------------
        // LOGIN
        // ---------------------
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string password)
        {
            if (string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Correo y contraseña son obligatorios.");
                return View();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Este correo no está registrado.");
                return View();
            }

            var hash = CrearHash(password);
            if (usuario.ContrasenaHash != hash)
            {
                ModelState.AddModelError("", "Contraseña incorrecta.");
                return View();
            }

            var perfil = await _context.PerfilesUsuarios.FirstOrDefaultAsync(p => p.UsuarioId == usuario.UsuarioId);

            // Guardar en sesión
            HttpContext.Session.SetString("UsuarioId", usuario.UsuarioId);
            HttpContext.Session.SetString("UsuarioCorreo", usuario.Correo);
            HttpContext.Session.SetString("UsuarioNombre", perfil?.NombreMostrar ?? usuario.Correo);

            // Autenticación con cookies
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.UsuarioId),
                new Claim(ClaimTypes.Email, usuario.Correo)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction("Index", "Home");
        }

        // ---------------------
        // LOGOUT
        // ---------------------
        [HttpGet]
        public IActionResult Logout()
        {
            var correo = HttpContext.Session.GetString("UsuarioCorreo");
            var nombre = HttpContext.Session.GetString("UsuarioNombre");

            ViewBag.Usuario = nombre ?? correo;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirm()
        {
            // Limpiar sesión y cookies
            HttpContext.Session.Clear();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Account");
        }

        // ---------------------
        // HASH PASSWORD
        // ---------------------
        private string CrearHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
