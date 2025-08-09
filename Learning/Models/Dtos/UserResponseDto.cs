using Learning.Models.Entities;

namespace Learning.Models.Dtos
{
    public class UserResponseDto
    {
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DocumentNumber { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }

        public Role Role { get; set; }
    }
}
