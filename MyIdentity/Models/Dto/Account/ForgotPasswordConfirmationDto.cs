using System.ComponentModel.DataAnnotations;

namespace MyIdentity.Models.Dto.Account;

    public class ForgotPasswordConfirmationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

