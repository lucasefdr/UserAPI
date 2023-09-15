using IdentityAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IdentityAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult RegisterUser([FromBody] CreateUserDto userDto)
    {
        throw new NotImplementedException(); 
    }
}