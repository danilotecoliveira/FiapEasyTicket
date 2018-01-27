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

        public void Salvar(Reserva reserva)
        {
            if (_conexao.Find<Reserva>(reserva.ID) == null)
                _conexao.Insert(reserva);
            else
                _conexao.Update(reserva);
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
