    using System.ComponentModel.DataAnnotations;
using Learning.Common.Constants;
using Learning.Security.Validators;

namespace Learning.Models.Dtos
{
    public class CreateCourseDto
    {
        [Required]
        public int InstructorId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El título es obligatorio.")]
        [StringLength(SecurityConstant.TITLE_MAX_LENGTH, ErrorMessage = "El título no puede tener más de {1} caracteres.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(int.MaxValue, ErrorMessage = "La descripción no puede ser demasiado larga.")]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "La duración es obligatoria.")]
        [Range(1, int.MaxValue, ErrorMessage = "La duración debe ser un número positivo.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio.")]
        [RegularExpression(@"^(activo|inactivo|pendiente|en_progreso|finalizado|cancelado)$", ErrorMessage = "El estado debe ser uno de los siguientes: activo, inactivo, pendiente, en_progreso, finalizado, cancelado.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de inicio no es válida.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "La fecha de finalización es obligatoria.")]
        [DataType(DataType.Date, ErrorMessage = "La fecha de finalización no es válida.")]
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? DeletedAt { get; set; } = null;



    }
}
