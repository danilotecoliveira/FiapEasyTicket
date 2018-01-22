using System;
using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using FiapEasyTicket.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FiapEasyTicket
{
    public class LoginService
    {
        public async Task Login(Login login)
        {

            using (var client = new HttpClient())
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("email", login.Email),
                    new KeyValuePair<string, string>("senha", login.Password)
                });

                client.BaseAddress = new Uri("https://aluracar.herokuapp.com");

                var result = new HttpResponseMessage();
                try
                {
                    result = await client.PostAsync("/login", content);
                }
                catch
                {
                    MessagingCenter.Send(new LoginException("Sem conexão"), "login-failed");
                }

                if (result.IsSuccessStatusCode)
                {
                    var cont = await result.Content.ReadAsStringAsync();
                    var resultLogin = JsonConvert.DeserializeObject<ResultadoLogin>(cont);
                    MessagingCenter.Send(resultLogin.Usuario, "login-success");
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
