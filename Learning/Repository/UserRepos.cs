using AutoMapper;
using BCrypt.Net;
using Learning.Data;
using Learning.Models.Dtos;
using Learning.Models.Entities;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Learning.Repository
{
    public class UserRepos : IUserRepos
    {
        private readonly DataContext _db;
        private IMapper _mapper;
        private readonly ILogger<UserRepos> _logger;
        public UserRepos(DataContext db, IMapper mapper, ILogger<UserRepos> logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CreateUserDto> CreateUser(CreateUserDto userDto)
        {
            try
            {
        
                var conficts = await _db.Users
                    .Where(u => 
                  
                    u.Email == userDto.Email ||
                    u.Username == userDto.UserName ||
                    u.DocumentNumber == userDto.DocumentNumber
                    )
                    
                    .Select(u => new {u.Email, u.Username, u.DocumentNumber} )                    
                    .ToListAsync();

                var errorMessages = new List<string>
                {
                    conficts.Any(u => u.Email == userDto.Email) ? "El correo electrónico ya está registrado." : string.Empty,
                    conficts.Any(u => u.Username == userDto.UserName) ? "El nombre de usuario ya está en uso." :  string.Empty,
                    conficts.Any(u => u.DocumentNumber == userDto.DocumentNumber) ? "El número de documento ya está registrado." :  string.Empty
                }
                .Where(m => !string.IsNullOrEmpty(m))
                .ToList(); 

                if (errorMessages.Any())
                    throw new ArgumentException(string.Join(" ", errorMessages));
                userDto.AvatarUrl ??= "/images/default-avatar.png"; 

                var user = _mapper.Map<User>(userDto);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                user.CreatedAt = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                return _mapper.Map<User, CreateUserDto>(user);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                throw;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear usuario");
                throw new Exception("Error inesperado al crear usuario", ex);
            }
        }

        public async Task<List<UserResponseDto>> SeeUsers()
        {
            try
            {
                var list = await _db.Users
                    .Where(u => u.DeletedAt == null)
                    .Include(u => u.Role)
                    .ToListAsync();

                return _mapper.Map<List<UserResponseDto>>(list);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener usuarios");
                throw new Exception("Error inesperado al obtener usuarios", ex);
            }
        }

        public async Task<UserResponseDto> GetUserById(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("El ID de usuario debe ser mayor que cero.");
            }

            try
            {
                var user = await _db.Users
                    .Where(u => u.Id == userId &&  u.DeletedAt == null)
                    .Include(u => u.Role)
                    .FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new KeyNotFoundException($"Usuario con id {userId} no encontrado.");
                }
                return _mapper.Map<UserResponseDto>(user);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (KeyNotFoundException)
            {
                throw;
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al obtener usuario por ID");
                throw;
            }
        }

        public async Task<UpdateUserDto> UpdateUser(int userId, UpdateUserDto userDto)
        {
      
            try
            {
                      var user = await _db.Users.FindAsync(userId);
            if (user == null)
            {
                throw new KeyNotFoundException("usuario no encontrado");
            }
                var conficts = await _db.Users
                    .Where(u => 
                     u.DeletedAt == null &&
                    (u.Email == userDto.Email ||
                    u.Username == userDto.UserName ||
                    u.DocumentNumber == userDto.DocumentNumber
                    ))
                    .Select(u => new { u.Email, u.Username, u.DocumentNumber })

                    .ToListAsync();

                var errorMessages = new List<string>
                {
                    conficts.Any(u => u.Email == userDto.Email) ? "El correo electrónico ya está registrado." : string.Empty,
                    conficts.Any(u => u.Username == userDto.UserName) ? "El nombre de usuario ya está en uso." :  string.Empty,
                    conficts.Any(u => u.DocumentNumber == userDto.DocumentNumber) ? "El número de documento ya está registrado." :  string.Empty
                }
                .Where(m => !string.IsNullOrWhiteSpace(m))
                .ToList();

                if (errorMessages.Any())
                    throw new ArgumentException(string.Join(" ", errorMessages));

                _mapper.Map(userDto, user);

                if(!string.IsNullOrWhiteSpace(userDto.Password))
                {
                    user!.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                }

                user!.UpdatedAt = DateTime.UtcNow;
                await _db.SaveChangesAsync();
                
                return _mapper.Map<UpdateUserDto>(user);

            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex, "Error al actualizar usuario");
                throw;
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, "Usuario no encontrado");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar usuario");
                throw new Exception("Error inesperado al actualizar usuario", ex);
            }
        }
        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == userId && u.DeletedAt == null);
                if (user == null)
                {
                    return false;
                }
                user.DeletedAt = DateTime.UtcNow;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar usuario");
                throw;
            }
        }

    }

}

