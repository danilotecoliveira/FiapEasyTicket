using System.Collections.ObjectModel;
using System.Linq;
using FiapEasyTicket.Data;
using FiapEasyTicket.Models;
using Xamarin.Forms;

namespace FiapEasyTicket.ViewModels
{
    public class AgendamentosUsuarioViewModel : BaseViewModel
    {
        private Agendamento agendamentoSelecionado;
        public Agendamento AgendamentoSelecionado
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

        public ObservableCollection<Agendamento> lista = new ObservableCollection<Agendamento>();
        public ObservableCollection<Agendamento> Lista
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

        public AgendamentosUsuarioViewModel()
        {
            AtualizarLista();
        }

        public void AtualizarLista()
        {
            using (var conexao = DependencyService.Get<ISQLite>().PegarConexao())
            {
                var dao = new AgendamentoDAO(conexao);
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
