namespace FiapEasyTicket.Models
{
    public class Login
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        public Login(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
