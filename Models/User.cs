using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace IdentityAPI.Models;

public class User : IdentityUser
{
    public DateTime DateOfBirth { get; set; }

    // Usuário terá as propriedades do IdentityUser
    public User() : base()
    {
    }
}