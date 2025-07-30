using Learning.Models;

namespace Learning.Repository
{
    public interface ILoginRepos
    {
        Task<(bool Success, string Message, User? User)> Login(string email, string password);
    }
}
