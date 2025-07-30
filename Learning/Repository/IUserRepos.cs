using Learning.Models;
using Learning.Models.Dtos;

namespace Learning.Repository

{
    public interface IUserRepos
    {
        Task<CreateUserDto> CreateUser(CreateUserDto user);


    }
}
