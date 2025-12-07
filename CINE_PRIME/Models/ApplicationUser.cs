using Microsoft.AspNetCore.Identity;
using System;

namespace CINE_PRIME.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string? ImagenPerfil { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now; 

    }

}
