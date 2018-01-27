using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FiapEasyTicket.Models;
using FiapEasyTicket.ViewModels;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
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
            MessagingCenter.Subscribe<Filme>(this, "FilmeSelecionado",
                (veiculo) =>
                {
                    Navigation.PushAsync(new DetalheView(veiculo, Usuario));
                }
            );

            await ViewModel.GetFilmes();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Filme>(this, "FilmeSelecionado");
        }
    }
}