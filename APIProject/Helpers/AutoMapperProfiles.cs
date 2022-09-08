using APIProject.Dtos;
using APIProject.Models;
using AutoMapper;

namespace APIProject.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, LoginDto>();
            CreateMap<LoginDto, UserModel>();
        }

    }
}
