using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FiapEasyTicket.ViewModels;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ReservasUsuarioView : ContentPage
	{
        readonly ReservasUsuarioViewModel _viewModel;

        public ReservasUsuarioView()
        {
            InitializeComponent();
            _viewModel = new ReservasUsuarioViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Reserva>(this, "ReservaSelecionada", async (reserva) =>
            {
                if (!reserva.Confirmado)
                {
                    var reenviar = await DisplayAlert("Reenviar", "Deseja reenviar os dados da reserva?", "Sim", "Não");

                    if (reenviar)
                    {
                        ReservaService service = new ReservaService();
                        service.EnviarReserva(reserva);
                        _viewModel.AtualizarLista();
                    }
                }
            });

            MessagingCenter.Subscribe<Reserva>(this, "SucessoReserva", async (reserva) => {
                await DisplayAlert("Reenviar", "Reenvio com sucesso!", "Ok");
            });

            MessagingCenter.Subscribe<Reserva>(this, "FalhaReserva", async (reserva) => {
                await DisplayAlert("Reenviar", "Falha ao reenvivar!", "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Reserva>(this, "ReservaSelecionada");
            MessagingCenter.Unsubscribe<Reserva>(this, "SucessoReserva");
            MessagingCenter.Unsubscribe<Reserva>(this, "FalhaReserva");
        }
    }
}