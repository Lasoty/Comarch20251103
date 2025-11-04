using ComarchCwiczenia.Client.Common;
using System.ComponentModel.DataAnnotations;

namespace ComarchCwiczenia.Client.Model;

public class Person
{
    [Required]
    public string Name { get; set; }

    [Range(18, 99, ErrorMessage = "Please enter a valid age!")]
    public int Age { get; set; }

    [Required(ErrorMessage = "Please enter your email!")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address!")]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$")]
    public string Email { get; set; }

    [Compare(nameof(Email), ErrorMessage = "Confirm email must match!")]
    public string ConfirmEmail { get; set; }

    [PasswordValidation(3, 50, true, true, true, true)]
    public string Password { get; set; }

    [Required, StringLength(15, MinimumLength = 5)]
    public string Address { get; set; }
}
