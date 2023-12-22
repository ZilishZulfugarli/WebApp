using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Models;
public class RegisterVM
{
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string? LastName { get; set; }
    [Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
    public string? Email { get; set; }
    [Required]
    [DataType(DataType.Password, ErrorMessage = "Password is wrong")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name ="Confirm Password")]
    [Compare("Password",ErrorMessage ="Password and confirmation password not match.")]
    public string ConfirmPassword { get; set; }
}