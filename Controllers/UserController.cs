using AutoMapper;
using IdentityAPI.Data.DTOs;
using IdentityAPI.Models;
using IdentityAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _service;

    public UserController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto userDto)
    {
        await _service.Register(userDto);
        return Ok("User registered.");
    }
}