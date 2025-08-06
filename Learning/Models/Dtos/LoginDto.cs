using Learning.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Learning.Models.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "El usuario o correo es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string UsernameOrEmail { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(SecurityConstant.PASSWORD_MAX_LENGTH, MinimumLength = SecurityConstant.PASSWORD_MIN_LENGTH, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        [RegularExpression(SecurityConstant.PASSWORD_PATTERN, ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
        public string Password { get; set; }
    }
}
