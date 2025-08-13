using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning.Models.Entities
{
    [Table("course")]
    public class Course
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("instructors")]
        [Column("instrcutor_id")]
        [Required]
        public int InstructorId { get; set; }
        public User Instructor { get; set; }

        [ForeignKey("categories")]
        [Column("category_id")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Column("title")]
        [Required]
        public string Title { get; set; }

        [Column("description", TypeName = "text")]
        [Required]
        public string Description { get; set; }

        [Column("avatar_url")]
        [Required]
        public string ImageUrl { get; set; }

        [Column("duration")]
        [Required]
        public int Duration { get; set; }

        [Column("status")]
        [Required]
        public string Status { get; set; }

        [Column("start_date")]
        [Required]
        public DateTime StartDate { get; set; }

        [Column("end_date")]
        [Required]
        public DateTime EndDate { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }


    }
}
