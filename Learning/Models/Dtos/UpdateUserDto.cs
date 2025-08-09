using System.ComponentModel.DataAnnotations;
using Learning.Common.Constants;
using Learning.Security.Validators;

namespace Learning.Models.Dtos
{
    public class UpdateUserDto
    {


        [Required]
      
        public int RoleId { get; set; }

        [StringLength(SecurityConstant.NAME_MAX_LENGTH, ErrorMessage = "El Nombre no puede contener mas {1} caracteres.")]
        [RegularExpression(SecurityConstant.NAME_PATTERN, ErrorMessage = "El nombre solo puede contener letras, espacios y algunos caracteres especiales como apóstrofes o guiones.")]
        public string? FirstName { get; set; }

        [StringLength(SecurityConstant.NAME_MAX_LENGTH, ErrorMessage = "El Apellido no puede contener mas {1} caracteres.")]
        [RegularExpression(SecurityConstant.NAME_PATTERN, ErrorMessage = "El apellido solo puede contener letras, espacios y algunos caracteres especiales como apóstrofes o guiones.")]
        public string? LastName { get; set; }
        public int? DocumentTypeId { get; set; }

        [StringLength(SecurityConstant.DOCUMENT_NUMBER_MAX_LENGTH, ErrorMessage = "El número de documento no puede tener más de {1} caracteres.")]
        [RegularExpression(SecurityConstant.DOCUMENT_NUMBER_PATTERN, ErrorMessage = "El número de documento contiene caracteres inválidos.")]
        public string? DocumentNumber { get; set; }

        [StringLength(SecurityConstant.USERNAME_MAX_LENGTH, ErrorMessage = "El nombre de usuario no puede tener más de {1} caracteres.")]
        [RegularExpression(SecurityConstant.USERNAME_PATTERN, ErrorMessage = "El nombre de usuario no es válido.")]
        public string? UserName { get; set; }

        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]    
        [StringLength(SecurityConstant.EMAIL_MAX_LENGTH, ErrorMessage = "El correo electrónico no puede tener más de {1} caracteres.")]
        public string? Email { get; set; }

        [StringLength(SecurityConstant.PASSWORD_MAX_LENGTH, MinimumLength = SecurityConstant.PASSWORD_MIN_LENGTH, ErrorMessage = "La contraseña debe tener entre {2} y {1} caracteres.")]
        [RegularExpression(SecurityConstant.PASSWORD_PATTERN, ErrorMessage = "La contraseña debe tener al menos una mayúscula, una minúscula, un número y un carácter especial.")]
        public string? Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden.")]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.Date, ErrorMessage = "La fecha de nacimiento no es válida.")]
        [AgeRange]
        public DateTime? BirthDate { get; set; }

        [StringLength(SecurityConstant.ADRESS_MAX_LENGTH, ErrorMessage = "La dirección no puede tener más de {1} caracteres.")]
        public string? Address { get; set; }

        public string? AvatarUrl { get; set; }

    }
}
