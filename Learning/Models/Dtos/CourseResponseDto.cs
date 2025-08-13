using Learning.Models.Entities;

namespace Learning.Models.Dtos
{
    public class CourseResponseDto
    {
        public int InstructorId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Duration { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Category Category { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
