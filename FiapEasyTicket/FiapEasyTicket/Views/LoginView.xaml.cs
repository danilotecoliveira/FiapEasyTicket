using System;
using Xamarin.Forms;

namespace FiapEasyTicket.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<LoginException>(this, "login-failed", async (ex) =>
            {
                await DisplayAlert("Login", ex.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<LoginException>(this, "FalhaLoain");
        }
    }
}