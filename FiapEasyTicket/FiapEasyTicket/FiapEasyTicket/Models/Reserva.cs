using SQLite;
using System;

namespace FiapEasyTicket.Models
{
    public class Reserva
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nome { get; set; }
        public string Fone { get; set; }
        public string Email { get; set; }
        public string Modelo { get; set; }
        public decimal Preco { get; set; }
        public bool Confirmado { get; set; }
        public string DataFormatada
        {
            get
            {
                return DataReserva.Add(HoraReserva).ToString("dd/MM/yyyy HH:mm");
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

        public TimeSpan HoraReserva { get; set; }

        public Reserva(string nome, string fone, string email, string modelo, decimal preco, DateTime dataReserva, TimeSpan horaReserva)
            : this(nome, fone, email, modelo, preco)
        {
            DataReserva = DataReserva;
            HoraReserva = HoraReserva;
        }

        public Reserva(string nome, string fone, string email, string modelo, decimal preco)
        {
            Nome = nome;
            Fone = fone;
            Email = email;
            Modelo = modelo;
            Preco = preco;
        }

        public Reserva() { }
    }
}
