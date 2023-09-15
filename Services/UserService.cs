using AutoMapper;
using IdentityAPI.Data.DTOs;
using IdentityAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Services;

public class UserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task Register(CreateUserDto userDto)
    {
        // Mapeia o DTO para usuário
        var user = _mapper.Map<User>(userDto);

        // Cadastrando usuário pelo Identity
        var result = await _userManager.CreateAsync(user, userDto.Password);

        // Retorna exceção se o cadastro não for efetuado corretamente
        if (!result.Succeeded) throw new ApplicationException("Register error");
    }

    public async Task Login(LoginUserDto userDto)
    {
        // username, password, cookie, fail
        var result = await _signInManager.PasswordSignInAsync(userDto.Username, userDto.Password, false, false);

        if (!result.Succeeded) throw new ApplicationException("Login error");
    }
}