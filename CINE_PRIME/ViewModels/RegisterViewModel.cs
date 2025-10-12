using System.ComponentModel.DataAnnotations;

namespace CINE_PRIME.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }

    }


}
