public class AuthResponseDto
{
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public UserResponseDto User { get; set; }
}