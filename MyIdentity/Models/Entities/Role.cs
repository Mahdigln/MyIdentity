using Microsoft.AspNetCore.Identity;

namespace MyIdentity.Models.Entities;

public class Role:IdentityRole
{
    public string Description { get; set; }
}