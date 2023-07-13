using System.ComponentModel.DataAnnotations;

namespace MyIdentity.Models.Dto;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public string UserName { get; set; } //UserName=Email
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool IsPersistent { get; set; } = false;

    public string ReturnUrl { get; set; }
}

