using Learning.Models.Entities;

namespace Learning.Repository
{
    public interface ILoginRepos
    {
        Task<(bool Success, string Message, User? User)> Login(string email, string password);
    }
}
