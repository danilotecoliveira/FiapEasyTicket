using System;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FiapEasyTicket
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {

            using (var client = new HttpClient())
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
                    MessagingCenter.Send(new LoginException("Sem conexão"), "FalhaLogin");
                }

                if (resultado.IsSuccessStatusCode)
                {
                    var conteudo = await resultado.Content.ReadAsStringAsync();
                    var resultadoLogin = JsonConvert.DeserializeObject<ResultadoLogin>(conteudo);
                    MessagingCenter.Send(resultadoLogin.Usuario, "SucessoLogin");
                }
                else
                    MessagingCenter.Send(new LoginException("Credencial incorreta"), "FalhaLogin");
            }
        }
    }

    public class LoginException : Exception
    {
        public LoginException(string message) : base(message) { }
    }
}
