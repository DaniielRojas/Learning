using Learning.Data;
using Learning.Models;
using Microsoft.EntityFrameworkCore;

namespace Learning.Repository
{
    public class LoginRepos : ILoginRepos
    {
        private readonly DataContext _db;

        public LoginRepos(DataContext db)
        {
            _db = db;
        }

        public async Task<(bool Success, string Message, User? User)> Login(string usernameoremail, string password)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == usernameoremail || u.Email == usernameoremail);

                if (user == null)
                {
                    return (false, "El usuario o correo no está registrado.", null);
                }

                if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
                    return (false, "La contraseña es incorrecta.", null);

                return (true, $"Bienvenido, {user.FirstName}", user);
            }
            catch (Exception ex)
            {
                return (false, $"Error al iniciar sesión: {ex.Message}", null);
            }
        }
    }
}