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
            {
                var usuaro = new Usuario { Nome = "Aluno Fiap", Email = "aluno@fiap.com.br" };
                MessagingCenter.Send(usuaro, "SucessoLogin");
            }
            else
                MessagingCenter.Send(new LoginException("Credencial incorreta"), "FalhaLogin");
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}
