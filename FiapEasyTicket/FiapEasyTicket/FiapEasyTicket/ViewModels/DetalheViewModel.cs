using Xamarin.Forms;
using System.Windows.Input;
using FiapEasyTicket.Models;

namespace FiapEasyTicket.ViewModels
{
    public class DetalheViewModel : BaseViewModel
    {
        public Filme Filme { get; set; }
        public ICommand ProximoComando { get; set; }

        public string Pipoca
        {
            get
            {
                return string.Format("Pipoca - R$ {0}", Filme._PIPOCA);
            }
        }

        public string Chocolate
        {
            get
            {
                return string.Format("Chocolate - R$ {0}", Filme._CHOCOLATE);
            }
        }

        public string Refrigerante
        {
            get
            {
                return string.Format("Refrigerante - R$ {0}", Filme._REFRIGERANTE);
            }
        }

        public bool TemPipoca
        {
            get
            {
                return Filme.TemPipoca;
            }
            set
            {
                Filme.TemPipoca = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemChocolate
        {
            get
            {
                return Filme.TemChocolate;
            }
            set
            {
                Filme.TemChocolate = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorTotal));
            }
        }

        public bool TemRefrigerante
        {
            get
            {
                return Filme.TemRefrigerante;
            }
            set
            {
                Filme.TemRefrigerante = value;
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

        public DetalheViewModel(Filme filme)
        {
            Filme = filme;
            ProximoComando = new Command(() =>
            {
                MessagingCenter.Send(filme, "Proximo");
            });
        }
    }
}
