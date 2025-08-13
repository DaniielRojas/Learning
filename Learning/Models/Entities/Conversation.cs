using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learning.Models.Entities
{
    [Table("conversation")]
    public class Conversation
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("conversations")]
        [Column("conersation_type_id")]
        [Required]
        public int ConversationTypeId { get; set; }
        public ConversationType ConversationType { get; set; }

        [Column("started_at")]
        [Required]
        public DateTime StartedAt { get; set; }

        [Column("status")]
        [Required]
        public string Status { get; set; }


        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
