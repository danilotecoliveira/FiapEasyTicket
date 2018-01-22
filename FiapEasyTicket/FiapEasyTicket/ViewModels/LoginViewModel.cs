using Xamarin.Forms;
using FiapEasyTicket.Models;
using System.Windows.Input;

namespace FiapEasyTicket.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string email = "joao@alura.com.br";
        public string Email
        {
            get { return email; }
            set {
                email = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private string password = "alura123";
        public string Password
        {
            get { return password; }
            set {
                password = value;
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new Command( async () =>
            {
                var loginService = new LoginService();
                await loginService.Login(new Login(email, password));
            },
                () => 
                {
                    return (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password));
                }
            );
        }
    }
}
