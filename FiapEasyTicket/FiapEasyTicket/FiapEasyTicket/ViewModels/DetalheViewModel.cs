using Xamarin.Forms;
using System.Windows.Input;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Filme Filme { get; set; }
        public ICommand ProximoComando { get; set; }

        public string FreioABS
        {
            get
            {
                return string.Format("Freio ABS - R$ {0}", Filme._freioABS);
            }
        }

        public string ArCondicionado
        {
            get
            {
                return string.Format("Ar condicionado - R$ {0}", Filme._arCondicionado);
            }
        }

        public string MP3Player
        {
            get
            {
                return string.Format("Mp3 Player - R$ {0}", Filme._mp3);
            }
        }

        public bool TemFreioABS
        {
            get
            {
                return Filme.TemFreioAbs;
            }
            set
            {
                Filme.TemFreioAbs = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemArCondicionado
        {
            get
            {
                return Filme.TemArCondicionado;
            }
            set
            {
                Filme.TemArCondicionado = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemMP3Player
        {
            get
            {
                return Filme.TemMP3Player;
            }
            set
            {
                Filme.TemMP3Player = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public string ValorTotal
        {
            get
            {
                return Filme.PrecoTotalFormatado;
            }
        }

        public DetalheViewModel(Filme veiculo)
        {
            Filme = veiculo;
            ProximoComando = new Command(() =>
            {
                MessagingCenter.Send(veiculo, "Proximo");
            });
        }
    }
}
