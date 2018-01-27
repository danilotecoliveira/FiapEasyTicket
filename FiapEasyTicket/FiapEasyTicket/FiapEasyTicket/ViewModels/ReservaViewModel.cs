using System;
using System.Text;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Windows.Input;
using FiapEasyTicket.Models;
using System.Threading.Tasks;

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
                MessagingCenter.Send(Reserva, "Agendamento");
            }, () =>
            {
                return
                !string.IsNullOrWhiteSpace(Nome) &&
                !string.IsNullOrWhiteSpace(Fone) &&
                !string.IsNullOrWhiteSpace(Email);
            });
        }


        private string Modelo;
        public string modelo
        {
            get { return Reserva.Nome; }
            set { Reserva.Nome = value; }
        }

        private decimal Preco;

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

        public DateTime DataAgendamento
        {
            get
            {
                return Reserva.DataAgendamento;
            }
            set
            {
                Reserva.DataAgendamento = value;
            }
        }

        public TimeSpan HoraAgendamento
        {
            get
            {
                return Reserva.HoraAgendamento;
            }
            set
            {
                Reserva.HoraAgendamento = value;
            }
        }

        public async void SalvarAgendamento()
        {
            ReservaService servico = new ReservaService();
            await servico.EnviarAgendamento(Reserva);
        }
    }

    public class AgendamentoService
    {
        const string URL_POST_AGENDAMENTO = "https://aluracar.herokuapp.com/salvaragendamento";

        public async Task EnviarAgendamento(Reserva agendamento)
        {
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(new
            {
                nome = agendamento.Nome,
                fone = agendamento.Fone,
                email = agendamento.Email,
                carro = agendamento.Modelo,
                preco = agendamento.Preco,
                dataAgendamento =
                new DateTime(
                    agendamento.DataAgendamento.Year,
                    agendamento.DataAgendamento.Month,
                    agendamento.DataAgendamento.Day,
                    agendamento.HoraAgendamento.Hours,
                    agendamento.HoraAgendamento.Minutes,
                    agendamento.HoraAgendamento.Seconds
                )
            });

            var conteudo = new StringContent(json, Encoding.UTF8, "application/json");
            var resposta = await client.PostAsync(URL_POST_AGENDAMENTO, conteudo);
            agendamento.Confirmado = resposta.IsSuccessStatusCode;

            SalvarAgendamentoDB(agendamento);

            if (agendamento.Confirmado)
                MessagingCenter.Send(agendamento, "SucessoAgendamento");
            else
                MessagingCenter.Send(new ArgumentException(), "FalhaAgendamento");
        }

        public void SalvarAgendamentoDB(Reserva agendamento)
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new ReservaDAO(conexao);
                dao.Salvar(agendamento);
            }
        }
    }
}
