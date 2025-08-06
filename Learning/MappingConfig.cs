using AutoMapper;
using Learning.Models.Dtos;
using Learning.Models.Entities;

namespace Learning
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CreateUserDto, User>();
                config.CreateMap<User, CreateUserDto>();
                config.CreateMap<UserResponseDto, CreateUserDto>();
                config.CreateMap<CreateUserDto, UserResponseDto>();
                config.CreateMap<User, UserResponseDto>();
                config.CreateMap<UserResponseDto, User>();
            });

            return mappingConfig;
        }
    }
}
