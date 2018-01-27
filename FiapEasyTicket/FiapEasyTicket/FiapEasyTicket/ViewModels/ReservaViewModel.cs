using System;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Input;
using FiapEasyTicket.Models;
using System.Threading.Tasks;
using FiapEasyTicket.Data;

namespace FiapEasyTicket.ViewModels
{
    public class ReservaViewModel : BaseViewModel
    {
        public Reserva Reserva { get; set; }
        public ICommand ComandoAgendar { get; set; }

        public ReservaViewModel(Filme veiculo, Usuario usuario)
        {
            Reserva = new Reserva(usuario.Nome, usuario.Telefone, usuario.Email, veiculo.Nome, veiculo.Preco);
            ComandoAgendar = new Command(() =>
            {
                MessagingCenter.Send(Reserva, "Reserva");
            }, () =>
            {
                return
                !string.IsNullOrWhiteSpace(Nome) &&
                !string.IsNullOrWhiteSpace(Fone) &&
                !string.IsNullOrWhiteSpace(Email);
            });
        }

        
        public string modelo
        {
            get { return Reserva.Nome; }
            set { Reserva.Nome = value; }
        }

        public decimal MyProperty
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

        public string Fone
        {
            get
            {
                return Reserva.Fone;
            }
            set
            {
                Reserva.Fone = value;
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

        public TimeSpan HoraReserva
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

        public async void SalvarReserva()
        {
            ReservaService servico = new ReservaService();
            await servico.EnviarReserva(Reserva);
        }
    }

    public class ReservaService
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvarreserva";

        public async Task EnviarReserva(Reserva reserva)
        {
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(new
            {
                nome = reserva.Nome,
                fone = reserva.Fone,
                email = reserva.Email,
                carro = reserva.Modelo,
                preco = reserva.Preco,
                dataReserva =
                new DateTime(
                    reserva.DataReserva.Year,
                    reserva.DataReserva.Month,
                    reserva.DataReserva.Day,
                    reserva.HoraReserva.Hours,
                    reserva.HoraReserva.Minutes,
                    reserva.HoraReserva.Seconds
                )
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            reserva.Confirmado = resposta.IsSuccessStatusCode;

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
