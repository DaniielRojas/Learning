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
                config.CreateMap<UpdateUserDto, User>();


                config.CreateMap<UpdateUserDto, User>()
                     .ForAllMembers(opt => opt.Condition((src, dest, srcMember) =>
                     {
                         if (srcMember == null) return false;
                         if (srcMember is string str)
                         {
                             return !string.IsNullOrWhiteSpace(str);
                         }
                         return true;
                     }));
                config.CreateMap<User, UpdateUserDto>();
                config.CreateMap<UserResponseDto, UpdateUserDto>();
                config.CreateMap<UpdateUserDto, UserResponseDto>();
               


            });

            return mappingConfig;
        }
    }
}
