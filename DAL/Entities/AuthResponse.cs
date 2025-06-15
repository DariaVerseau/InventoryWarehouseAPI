using DTO.User;

namespace DAL.Entities;


// Models/AuthResponse.cs
public class AuthResponse
{ 
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
        public UserDto User { get; set; }
}
