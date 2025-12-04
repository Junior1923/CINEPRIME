using System.ComponentModel.DataAnnotations;

namespace CINE_PRIME.ViewModels
{
    public class ProfileVM
    {
        // Datos a mostrar (readonly en la vista)
        public string UserId { get; set; } = string.Empty;

        [Display(Name = "Nombres")]
        public string Nombre { get; set; } = string.Empty;

        [Display(Name = "Apellidos")]
        public string Apellido { get; set; } = string.Empty;

        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; } = string.Empty;

        [Display(Name = "Registrado desde")]
        public DateTime FechaRegistro { get; set; }

        [Display(Name = "Foto de perfil")]
        public string? ImagenPerfil { get; set; }

        // Estadísticas
        [Display(Name = "Favoritos")]
        public int CantFavoritos { get; set; }

        [Display(Name = "Lista pendiente")]
        public int CantPendientes { get; set; }

        // Campos para editar (se usarán en el formulario de edición)
        [Display(Name = "Nombres")]
        public string EditNombre { get; set; } = string.Empty;

        [Display(Name = "Apellidos")]
        public string EditApellido { get; set; } = string.Empty;

        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string EditCorreo { get; set; } = string.Empty;

        // Campos para cambiar contraseña
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string? ContrasenaActual { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string? NuevaContrasena { get; set; }

        [DataType(DataType.Password)]
        [Compare("NuevaContrasena", ErrorMessage = "La nueva contraseña y la confirmación no coinciden.")]
        [Display(Name = "Confirmar nueva contraseña")]
        public string? ConfirmarContrasena { get; set; }
    }
}
