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

            MessagingCenter.Subscribe<Reserva>(this, "AgendamentoSelecionado", async (agendamento) =>
            {
                if (!agendamento.Confirmado)
                {
                    var reenviar = await DisplayAlert("Reenviar", "Deseja reenviar os dados do agendamento?", "Sim", "Não");

                    if (reenviar)
                    {
                        ReservaService service = new ReservaService();
                        await service.EnviarAgendamento(agendamento);
                        _viewModel.AtualizarLista();
                    }
                }
            });

            MessagingCenter.Subscribe<Reserva>(this, "SucessoAgendamento", async (agendamento) => {
                await DisplayAlert("Reenviar", "Reenvio com sucesso!", "Ok");
            });

            MessagingCenter.Subscribe<Reserva>(this, "FalhaAgendamento", async (agendamento) => {
                await DisplayAlert("Reenviar", "Falha ao sucesso!", "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Reserva>(this, "AgendamentoSelecionado");
            MessagingCenter.Unsubscribe<Reserva>(this, "SucessoAgendamento");
            MessagingCenter.Unsubscribe<Reserva>(this, "FalhaAgendamento");
        }
    }
}