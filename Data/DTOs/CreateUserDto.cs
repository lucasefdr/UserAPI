using System.ComponentModel.DataAnnotations;

namespace IdentityAPI.Data.DTOs;

public class CreateUserDto
{
    [Required] 
    public string Username { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }
    
    [Required] 
    public DateTime DateOfBirth { get; set; }
}