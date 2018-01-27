using Xamarin.Forms;
using FiapEasyTicket.Models;
using System.Collections.ObjectModel;
using FiapEasyTicket.Data;
using System.Linq;

namespace FiapEasyTicket.ViewModels
{
    public class ReservasUsuarioViewModel : BaseViewModel
    {
        private Reserva agendamentoSelecionado;
        public Reserva AgendamentoSelecionado
        {
            get { return agendamentoSelecionado; }
            set
            {
                if (value != null)
                {
                    agendamentoSelecionado = value;
                    MessagingCenter.Send(agendamentoSelecionado, "AgendamentoSelecionado");
                }
            }
        }

        public ObservableCollection<Reserva> lista = new ObservableCollection<Reserva>();
        public ObservableCollection<Reserva> Lista
        {
            get
            {
                return lista;
            }

            private set
            {
                lista = value;
                OnPropertyChanged();
            }
        }

        public ReservasUsuarioViewModel()
        {
            AtualizarLista();
        }

        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new ReservaDAO(conexao);
                var listaDB = dao.Lista;

                var query = listaDB
                    .OrderBy(m => m.DataAgendamento)
                    .ThenBy(m => m.HoraAgendamento);

                Lista.Clear();
                foreach (var item in query)
                {
                    lista.Add(item);
                }
            }
        }
    }
}
