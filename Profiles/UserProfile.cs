using AutoMapper;
using IdentityAPI.Data.DTOs;
using IdentityAPI.Models;

namespace IdentityAPI.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<CreateUserDto, User>();
    }
}