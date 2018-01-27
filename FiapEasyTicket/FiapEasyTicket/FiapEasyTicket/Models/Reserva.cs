using SQLite;
using System;

namespace FiapEasyTicket.Models
{
    public class Reserva
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Titulo { get; set; }
        public string Email { get; set; }
        public string Preco { get; set; }
        public bool Confirmado { get; set; }
        public string DataFormatada
        {
            get
            {
                return $"{DataReserva.ToString("dd/MM/yyyy")} {HoraReserva}";;
            }
        }

        DateTime dataReserva = DateTime.Today;
        public DateTime DataReserva
        {
            get
            {
                return dataReserva;
            }
            set
            {
                dataReserva = value;
            }
        }

        public string HoraReserva { get; set; }

        public Reserva(string nome, string email, string preco, string titulo, DateTime dataReserva, TimeSpan horaReserva)
            : this(nome, email, preco, titulo)
        {
            DataReserva = DataReserva;
            HoraReserva = HoraReserva;
        }

        public Reserva(string nome, string email, string preco, string titulo)
        {
            Nome = nome;
            Email = email;
            Preco = preco;
            Titulo = titulo;
        }

        public Reserva() { }
    }
}
