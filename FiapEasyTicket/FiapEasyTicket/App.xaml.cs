using FiapEasyTicket.Models;
using FiapEasyTicket.Views;
using Xamarin.Forms;

namespace FiapEasyTicket
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
            MessagingCenter.Subscribe<Usuario>(this, "login-success", (usuario) => 
            {
                //MainPage = new NavigationPage(new ListagemView());
                MainPage = new MasterDetailView(usuario);
            });
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
