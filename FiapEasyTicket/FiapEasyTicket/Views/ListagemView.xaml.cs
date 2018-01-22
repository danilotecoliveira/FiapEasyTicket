using FiapEasyTicket.Models;
using FiapEasyTicket.ViewModels;
using Xamarin.Forms;

namespace FiapEasyTicket.Views
{
    public partial class ListagemView : ContentPage
    {
        private ListagemViewModel ViewModel { get; set; }
        readonly public Usuario Usuario;

        public ListagemView(Usuario usuario)
        {
            InitializeComponent();
            ViewModel = new ListagemViewModel();
            BindingContext = ViewModel;
            Usuario = usuario;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Veiculo>(this, "VeiculoSelecionado", 
                (veiculo) => 
                {
                    Navigation.PushAsync(new DetalheView(veiculo, Usuario));
                }
            );

            await ViewModel.GetVeiculos();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Veiculo>(this, "VeiculoSelecionado");
        }
    }
}
