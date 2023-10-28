namespace Streamberry.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginModel()
        {
            Email = string.Empty;
            Password = string.Empty;
        }
    }
}
