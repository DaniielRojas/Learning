using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning.Models.Entities
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("document_type_id")]
        public int DocumentTypeId { get; set; }

        [ForeignKey("roles")]
        [Column("role_id")]
        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        [Column("first_name")]
        [Required]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        public string LastName { get; set; }

        [Column("document_number")]
        [Required]
        public string DocumentNumber { get; set; }

        [Column("username")]
        [Required]
        public string Username { get; set; }

        [Column("email")]
        [Required]
        public string Email { get; set; }

        [Column("birth_date")]
        [Required]
        public DateTime BirthDate { get; set; }

        [Column("password")]
        [Required]
        
        public string Password { get; set; }

        [Column("address")]
        [Required]
        public string Address { get; set; }

        [Column("avatar_url")]
        [Required]
        public string AvatarUrl { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }

    }
}
