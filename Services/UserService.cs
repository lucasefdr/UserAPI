using AutoMapper;
using IdentityAPI.Data.DTOs;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public UserService(IMapper mapper, UserManager<User> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task Register(CreateUserDto userDto)
    {
        // Mapeia o DTO para usuário
        var user = _mapper.Map<User>(userDto);

        // Cadastrando usuário pelo Identity
        var result = await _userManager.CreateAsync(user, userDto.Password);

        // Retorna exceção se o cadastro não for efetuado corretamente
        if (!result.Succeeded) throw new ApplicationException("Error");
    }
}