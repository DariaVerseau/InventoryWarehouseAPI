// DTO/Auth/RefreshTokenRequest.cs

using System.ComponentModel.DataAnnotations;

public class RefreshTokenRequest
{
    [Required]
    public string AccessToken { get; set; }

    [Required]
    public string RefreshToken { get; set; }
}