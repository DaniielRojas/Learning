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
        public UserRepos(DataContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<CreateUserDto> CreateUser(CreateUserDto userDto)
        {
            if (await _db.Users.AnyAsync(u => u.Email == userDto.Email))
                throw new ArgumentException("El correo electrónico ya está registrado.");

            if (await _db.Users.AnyAsync(u => u.Username == userDto.UserName))
                throw new ArgumentException("El nombre de usuario ya está en uso.");

            if (await _db.Users.AnyAsync(u => u.DocumentNumber == userDto.DocumentNumber))
                throw new ArgumentException("El número de documento ya está registrado.");

         
            var user = _mapper.Map<User>(userDto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            return _mapper.Map<User, CreateUserDto>(user);
        }
    }

}
