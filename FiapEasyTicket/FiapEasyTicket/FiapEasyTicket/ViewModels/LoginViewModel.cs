﻿using Xamarin.Forms;
using System.Windows.Input;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.ViewModels
{
    public class LoginViewModel
    {
        private string usuario = "aluno@fiap.com.br";
        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        private string senha = "fiap123";
        public string Senha
        {
            get { return senha; }
            set
            {
                senha = value;
                ((Command)EntrarCommand).ChangeCanExecute();
            }
        }

        public ICommand EntrarCommand { get; private set; }

        public LoginViewModel()
        {
            EntrarCommand = new Command(async () =>
            {
                var loginService = new LoginService();
                await loginService.FazerLogin(new Login(usuario, senha));
            },
                () =>
                {
                    return (!string.IsNullOrWhiteSpace(usuario) && !string.IsNullOrWhiteSpace(senha));
                }
            );
        }
    }
}
