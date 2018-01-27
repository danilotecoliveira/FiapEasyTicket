using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        private readonly Usuario _usuario;

        public MasterDetailView(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            Master = new MasterView(usuario);
            Detail = new NavigationPage(new ListagemView(usuario));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<Usuario>(this, "MinhasReservas", (usuario) =>
            {
                Detail = new NavigationPage(new ReservasUsuarioView());
                IsPresented = false;
            });

            MessagingCenter.Subscribe<Usuario>(this, "NovaReserva", (usuario) =>
            {
                Detail = new NavigationPage(new ListagemView(usuario));
                IsPresented = false;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Usuario>(this, "MinhasReservas");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovaReserva");
        }
    }
}