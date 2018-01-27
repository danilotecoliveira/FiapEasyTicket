using System.IO;
using Xamarin.Forms;
using System.Windows.Input;
using FiapEasyTicket.Media;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.ViewModels
{
    public class MasterViewModel : BaseViewModel
    {
        public ICommand EditarPerfilCommand { get; private set; }
        public ICommand SalvarCommand { get; private set; }
        public ICommand EditarCommand { get; private set; }
        public ICommand TirarFotoCommand { get; private set; }
        public ICommand MinhasReservasCommand { get; private set; }
        public ICommand NovaReservaCommand { get; private set; }

        public string Nome
        {
            get { return _usuario.Nome; }
            set { _usuario.Nome = value; }
        }

        public string Email
        {
            get { return _usuario.Email; }
            set { _usuario.Email = value; }
        }

        public string DataNascimento
        {
            get { return _usuario.DataNascimento; }
            set { _usuario.DataNascimento = value; }
        }

        public string Telefone
        {
            get { return _usuario.Telefone; }
            set { _usuario.Telefone = value; }
        }

        private bool editando = false;
        public bool Editando
        {
            get { return editando; }
            private set
            {
                editando = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fotoPerfil = "perfil.png";
        public ImageSource FotoPerfil
        {
            get { return fotoPerfil; }
            private set
            {
                fotoPerfil = value;
                OnPropertyChanged();
            }
        }

        private readonly Usuario _usuario;

        public MasterViewModel(Usuario usuario)
        {
            _usuario = usuario;

            EditarPerfilCommand = new Command(() =>
            {
                MessagingCenter.Send(usuario, "EditarPerfil");
            });

            SalvarCommand = new Command(() =>
            {
                Editando = false;
                MessagingCenter.Send(usuario, "SucessoSalvarUsuario");
            });

            EditarCommand = new Command(() =>
            {
                Editando = true;
            });

            TirarFotoCommand = new Command(() =>
            {
                DependencyService.Get<ICamera>().TirarFoto();
            });

            MessagingCenter.Subscribe<byte[]>(this, "FotoTirada", (bytes) =>
            {
                FotoPerfil = ImageSource.FromStream(() => new MemoryStream(bytes));
            });

            MinhasReservasCommand = new Command(() =>
            {
                MessagingCenter.Send(usuario, "MinhasReservas");
            });

            NovaReservaCommand = new Command(() =>
            {
                MessagingCenter.Send(usuario, "NovaReserva");
            });
        }
    }
}
