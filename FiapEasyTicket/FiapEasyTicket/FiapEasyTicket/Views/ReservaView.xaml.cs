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

        public ReservaView(Filme veiculo, Usuario usuario)
        {
            InitializeComponent();
            ViewModel = new ReservaViewModel(veiculo, usuario);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Reserva>(this, "Agendamento",
                async (msg) =>
                {
                    var confirma = await DisplayAlert("Salvar Agendamento",
                    "Deseja mesmo enviar o agendamento?",
                    "sim", "não");

                    if (confirma)
                    {
                        ViewModel.SalvarReserva();
                    }
                });

            MessagingCenter.Subscribe<Reserva>(this, "SucessoAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Agendamento salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<ArgumentException>(this, "FalhaAgendamento", async (msg) =>
            {
                await DisplayAlert("Agendamento", "Erro ao fazer o agendamento!", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Reserva>(this, "Agendamento");
            MessagingCenter.Unsubscribe<Reserva>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FalhaAgendamento");
        }
    }
}