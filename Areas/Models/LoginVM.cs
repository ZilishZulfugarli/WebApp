using System.ComponentModel.DataAnnotations;

namespace WebApp.Areas.Models;

public class LoginVM
{
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.EmailAddress, ErrorMessage = "Email is wrong.")]
    public string? Email { get; set; }
    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password, ErrorMessage = "Password is wrong")]
    public string? Password { get; set; }

    public bool RememberMe { get; set; }
}