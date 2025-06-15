using System.ComponentModel.DataAnnotations;

namespace DTO.User
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Username { get; set; }
        
        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}

/* "Jwt": {
    "Key": "your_super_secret_key_at_least_32_chars",
    "Issuer": "yourdomain.com",
    "Audience": "yourdomain.com",
    "AccessTokenExpirationMinutes": 60,
    "RefreshTokenExpirationDays": 7
  }*/