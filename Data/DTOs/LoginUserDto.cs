using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Data.DTOs;

public class LoginUserDto
{
    [Required] public string Username { get; set; }
    [Required] public string Password { get; set; }
}