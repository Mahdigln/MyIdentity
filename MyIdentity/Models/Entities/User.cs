using Microsoft.AspNetCore.Identity;

namespace MyIdentity.Models.Entities;

//dar halat adi id be sorat guid hast age bkhaim long bashe baiad public class User : IdentityUser<long> bashe 
public class User:IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}