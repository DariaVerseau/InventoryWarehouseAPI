// DTO/Auth/RegisterModel.cs

using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    public string? Role { get; set; } // Необязательное поле, по умолчанию "User"
}



