using Xamarin.Forms;
using FiapEasyTicket.Models;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace FiapEasyTicket.ViewModels
{
    public class ListagemViewModel : BaseViewModel
    {
        public ObservableCollection<Filme> Filmes { get; set; }
        const string URL = "http://aluracar.herokuapp.com/";

        private bool aguarde;
        public bool Aguarde
        {
            get { return aguarde; }
            set
            {
                aguarde = value;
                OnPropertyChanged();
            }
        }

        private Filme veiculoSelecionado;

        public Filme FilmeSelecionado
        {
            get { return veiculoSelecionado; }
            set
            {
                veiculoSelecionado = value;

                if (value != null)
                    MessagingCenter.Send(veiculoSelecionado, "FilmeSelecionado");
            }
        }

        public ListagemViewModel()
        {
            Filmes = new ObservableCollection<Filme>();
        }

        public async Task GetFilmes()
        {
            Aguarde = true;

            Filmes.Add(new Filme { Cartaz = "bastardosinglorios.jpg", Titulo = "Bastardos Inglórios", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "belezaamericana.jpg", Titulo = "Beleza Americana", Preco = 60 });
            Filmes.Add(new Filme { Cartaz = "cassino.jpg", Titulo = "Cassino", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "damadeferro.jpg", Titulo = "A Daman de Ferro", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "diadetrinamento.jpg", Titulo = "Um Dia de Treinamento", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "djangolivre.jpg", Titulo = "Django Livre", Preco = 35 });
            Filmes.Add(new Filme { Cartaz = "gladiador.jpg", Titulo = "Gladiador", Preco = 35 });
            Filmes.Add(new Filme { Cartaz = "homensdehonra.jpg", Titulo = "Homens de Honra", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "infiltrados.jpg", Titulo = "Os Infiltrados", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "jogodaimitacao.jpg", Titulo = "O Jogo da Imitação", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "killbill.jpg", Titulo = "Kill Bill", Preco = 45 });
            Filmes.Add(new Filme { Cartaz = "labirintodofauno.jpg", Titulo = "O Labirinto do Fauno", Preco = 50 });
            Filmes.Add(new Filme { Cartaz = "listadeshindler.jpg", Titulo = "A Lista de Schindler", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "oitoodiados.jpg", Titulo = "Os Oito Odiados", Preco = 40 });
            Filmes.Add(new Filme { Cartaz = "senhordosaneis.jpg", Titulo = "O Senhor dos Anéis", Preco = 50 });
            Filmes.Add(new Filme { Cartaz = "silenciodosinicentes.jpg", Titulo = "O Silêncio dos Inocentes", Preco = 40 });

            Aguarde = false;
        }

    }

    public class FilmeJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }
}
