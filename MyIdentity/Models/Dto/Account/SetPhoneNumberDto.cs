using System.ComponentModel.DataAnnotations;

namespace MyIdentity.Models.Dto.Account;

public class SetPhoneNumberDto
{
    [Required]
    [RegularExpression(@"(\+98|0)?9\d{9}")]// اینو میزاریم برای شماره موبایل های ایرانی
    public string PhoneNumber { get; set; }
}

