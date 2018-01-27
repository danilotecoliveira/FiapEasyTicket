using FiapEasyTicket.Models;
using FiapEasyTicket.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
	{
        public Filme Veiculo { get; set; }
        public Usuario Usuario { get; set; }

        public DetalheView(Filme veiculo, Usuario usuario)
        {
            InitializeComponent();
            Veiculo = veiculo;
            Usuario = usuario;
            BindingContext = new DetalheViewModel(veiculo);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Filme>(this, "Proximo", (veiculo) =>
            {
                Navigation.PushAsync(new ReservaView(veiculo, Usuario));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Filme>(this, "Proximo");
        }
    }
}