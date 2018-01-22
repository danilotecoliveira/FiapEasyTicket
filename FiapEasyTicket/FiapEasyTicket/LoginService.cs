using System;
using Xamarin.Forms;
using System.Net.Http;
using FiapEasyTicket.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FiapEasyTicket
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {

            using (HttpClient client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", login.Email),
                    new KeyValuePair<string, string>("senha", login.Senha)
                });

                client.BaseAddress = new Uri("https://aluracar.herokuapp.com");

                HttpResponseMessage resultado = new HttpResponseMessage();
                try
                {
                    resultado = await client.PostAsync("/login", content);
                }
                catch
                {
                    MessagingCenter.Send(new LoginException("Sem conexão"), "login-failed");
                }

                if (resultado.IsSuccessStatusCode)
                {
                    var conteudo = await resultado.Content.ReadAsStringAsync();
                    var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudo);
                    MessagingCenter.Send(resultadoLogin.Usuario, "login-success");
                }
                else
                    MessagingCenter.Send(new LoginException("Credencial incorreta"), "login-failed");
            }
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) {}
    }
}
