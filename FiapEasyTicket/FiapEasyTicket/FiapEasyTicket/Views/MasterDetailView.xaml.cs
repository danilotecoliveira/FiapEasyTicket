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

            MessagingCenter.Subscribe<Usuario>(this, "MeusAgendamentos", (usuario) =>
            {
                Detail = new NavigationPage(new ReservasUsuarioView());
                IsPresented = false;
            });

            MessagingCenter.Subscribe<Usuario>(this, "NovoAgendamentos", (usuario) =>
            {
                Detail = new NavigationPage(new ListagemView(usuario));
                IsPresented = false;
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            MessagingCenter.Unsubscribe<Usuario>(this, "MeusAgendamentos");
            MessagingCenter.Unsubscribe<Usuario>(this, "NovoAgendamentos");
        }
    }
}