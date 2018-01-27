using System;
using Xamarin.Forms;
using FiapEasyTicket.Data;
using System.Windows.Input;
using FiapEasyTicket.Models;
using System.Threading.Tasks;

namespace FiapEasyTicket.ViewModels
{
    public class ReservaViewModel : BaseViewModel
    {
        public Reserva Reserva { get; set; }
        public ICommand ComandoAgendar { get; set; }

        public ReservaViewModel(Filme filme, Usuario usuario)
        {
            Reserva = new Reserva(usuario.Nome, usuario.Email, filme.PrecoTotalFormatado, filme.Titulo);
            ComandoAgendar = new Command(() =>
            {
                MessagingCenter.Send(Reserva, "Reserva");
            }, () =>
            {
                return
                !string.IsNullOrWhiteSpace(Nome) &&
                !string.IsNullOrWhiteSpace(Email);
            });
        }

        
        public string modelo
        {
            get { return Reserva.Nome; }
            set { Reserva.Nome = value; }
        }

        public string Preco
        {
            get { return Reserva.Preco; }
            set { Reserva.Preco = value; }
        }

        public string Nome
        {
            get
            {
                return Reserva.Nome;
            }
            set
            {
                Reserva.Nome = value;
                OnPropertyChanged();
                ((Command)ComandoAgendar).ChangeCanExecute();
            }
        }

        public string Email
        {
            get
            {
                return Reserva.Email;
            }
            set
            {
                Reserva.Email = value;
                OnPropertyChanged();
                ((Command)ComandoAgendar).ChangeCanExecute();
            }
        }

        public DateTime DataReserva
        {
            get
            {
                return Reserva.DataReserva;
            }
            set
            {
                Reserva.DataReserva = value;
            }
        }

        public string HoraReserva
        {
            get
            {
                return Reserva.HoraReserva;
            }
            set
            {
                Reserva.HoraReserva = value;
            }
        }

        public void SalvarReserva()
        {
            ReservaService servico = new ReservaService();
            servico.EnviarReserva(Reserva);
        }
    }

    public class ReservaService
    {
        public void EnviarReserva(Reserva reserva)
        {
            reserva.Confirmado = true;
            SalvarReservaDB(reserva);

            if (reserva.Confirmado)
                MessagingCenter.Send(reserva, "SucessoReserva");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaReserva");
        }

        public void SalvarReservaDB(Reserva reserva)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new ReservaDAO(conexao);
                dao.Salvar(reserva);
            }
        }
    }
}
