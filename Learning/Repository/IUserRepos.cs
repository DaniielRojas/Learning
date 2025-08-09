using Learning.Models;
using Learning.Models.Dtos;

namespace Learning.Repository

{
    public interface IUserRepos
    {
        Task<CreateUserDto> CreateUser(CreateUserDto user);
        Task<List<UserResponseDto>> SeeUsers();
        Task<UserResponseDto> GetUserById(int id);
        Task<UpdateUserDto> UpdateUser(int id, UpdateUserDto userDto);
        Task<bool> DeleteUser(int userId);

    }
}
