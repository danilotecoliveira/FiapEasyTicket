using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FiapEasyTicket.Models;
using FiapEasyTicket.ViewModels;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReservaView : ContentPage
	{
        public ReservaViewModel ViewModel { get; set; }

        public ReservaView(Filme filme, Usuario usuario)
        {
            InitializeComponent();
            ViewModel = new ReservaViewModel(filme, usuario);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Reserva>(this, "Reserva",
                async (msg) =>
                {
                    var confirma = await DisplayAlert("Salvar Reserva",
                    "Deseja mesmo enviar a reserva?",
                    "sim", "não");

                    if (confirma)
                    {
                        ViewModel.SalvarReserva();
                    }
                });

            MessagingCenter.Subscribe<Reserva>(this, "SucessoReserva", async (msg) =>
            {
                await DisplayAlert("Reserva", "Reserva salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaReserva", async (msg) =>
            {
                await DisplayAlert("Reserva", "Erro ao fazer a reserva!", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Reserva>(this, "Reserva");
            MessagingCenter.Unsubscribe<Reserva>(this, "SucessoReserva");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaReserva");
        }
    }
}