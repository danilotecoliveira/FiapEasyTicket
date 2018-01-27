using FiapEasyTicket.Models;
using FiapEasyTicket.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalheView : ContentPage
	{
        public Filme Filme { get; set; }
        public Usuario Usuario { get; set; }

        public DetalheView(Filme filme, Usuario usuario)
        {
            InitializeComponent();
            Filme = filme;
            Usuario = usuario;
            BindingContext = new DetalheViewModel(filme);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Filme>(this, "Proximo", (filme) =>
            {
                Navigation.PushAsync(new ReservaView(filme, Usuario));
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Filme>(this, "Proximo");
        }
    }
}