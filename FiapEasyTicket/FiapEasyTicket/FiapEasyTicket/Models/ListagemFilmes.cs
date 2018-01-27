using System.Collections.Generic;

namespace FiapEasyTicket.Models
{
    public class ListagemFilmes
    {
        public List<Filme> Filmes { get; set; }

        public ListagemFilmes()
        {
            Filmes = new List<Filme>
            {
                new Filme { Nome = "Azera V6", Preco = 60000 },
                new Filme { Nome = "Fiesta 2.0", Preco = 50000 },
                new Filme { Nome = "HB20 s", Preco = 40000 }
            };
        }
    }
}
