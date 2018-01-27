using SQLite;
using System.Linq;
using FiapEasyTicket.Models;
using System.Collections.Generic;

namespace FiapEasyTicket.Data
{
    public class ReservaDAO
    {
        private readonly SQLiteConnection _conexao;

        public ReservaDAO(SQLiteConnection conexao)
        {
            _conexao = conexao;
            _conexao.CreateTable<Reserva>();
        }

        public void Salvar(Reserva agendamento)
        {
            if (_conexao.Find<Reserva>(agendamento.ID) == null)
                _conexao.Insert(agendamento);
            else
                _conexao.Update(agendamento);
        }

        private List<Reserva> lista;

        public List<Reserva> Lista
        {
            get
            {
                return _conexao.Table<Reserva>().ToList();
            }
            set { lista = value; }
        }
    }
}
