using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CINE_PRIME.Models;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CINE_PRIME.Controllers
{
    public class AccountController : Controller
    {
        private readonly cineprimeContext _context;

        public AccountController(cineprimeContext context)
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
        public async Task<IActionResult> Register(string usuarioId, string correo, string password)
        {
            if (string.IsNullOrEmpty(usuarioId) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View();
            }

            // Verificar si el correo ya existe
            var usuarioExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Correo == correo);
            if (usuarioExistente != null)
            {
                ModelState.AddModelError("", "Este correo ya está registrado.");
                return View();
            }

            var hash = CrearHash(password);

            var nuevoUsuario = new Usuario
            {
                UsuarioId = usuarioId,
                Correo = correo,
                ContrasenaHash = hash,
                FechaRegistro = DateTime.Now
            };

            // Crear el perfil asociado al usuario
            var perfil = new PerfilesUsuario
            {
                UsuarioId = usuarioId,
                NombreMostrar = usuarioId, // puedes cambiar a un nombre real si lo pides en el registro
                UrlAvatar = null // puedes poner un avatar por defecto
            };

            // Agregar ambos al contexto
            _context.Usuarios.Add(nuevoUsuario);
            _context.PerfilesUsuarios.Add(perfil);

            await _context.SaveChangesAsync();

            // Loguear automáticamente
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, nuevoUsuario.UsuarioId),
        new Claim(ClaimTypes.Email, nuevoUsuario.Correo)
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
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
                ModelState.AddModelError("", "Este correo no está registrado. Por favor, crea una cuenta.");
                return View();
            }

            var hash = CrearHash(password);
            if (usuario.ContrasenaHash != hash)
            {
                ModelState.AddModelError("", "Contraseña incorrecta.");
                return View();
            }

            // Loguear con cookies
            var claims = new List<Claim>
            {
                
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
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        // ---------------------
        // HASH PASSWORD
        // ---------------------
        private string CrearHash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(input);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
