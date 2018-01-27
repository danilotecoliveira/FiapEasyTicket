﻿using Xamarin.Forms;
using FiapEasyTicket.Views;
using FiapEasyTicket.Models;

namespace FiapEasyTicket
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new LoginView();
        }

		protected override void OnStart ()
		{
            MessagingCenter.Subscribe<Usuario>(this, "SucessoLogin", (usuario) =>
            {
                MainPage = new MasterDetailView(usuario);
            });
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
