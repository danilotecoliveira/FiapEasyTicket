using System;
using Xamarin.Forms;
using FiapEasyTicket.Models;
using System.Threading.Tasks;

namespace FiapEasyTicket
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {
            if (login.Email == "aluno@fiap.com.br" && login.Senha == "fiap123")
                MessagingCenter.Send(new Usuario(), "SucessoLogin");
            else
                MessagingCenter.Send(new LoginException("Credencial incorreta"), "FalhaLogin");
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}
