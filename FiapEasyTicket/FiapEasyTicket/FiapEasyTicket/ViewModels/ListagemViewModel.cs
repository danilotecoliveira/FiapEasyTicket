using Xamarin.Forms;
using System.Net.Http;
using Newtonsoft.Json;
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
            HttpClient client = new HttpClient();
            var resultado = await client.GetStringAsync(URL);
            var veiculosJson = JsonConvert.DeserializeObject<FilmeJson[]>(resultado);

            foreach (var item in veiculosJson)
            {
                Filmes.Add(new Filme
                {
                    Nome = item.nome,
                    Preco = item.preco
                });
            }

            Aguarde = false;
        }

    }

    public class FilmeJson
    {
        public string nome { get; set; }
        public int preco { get; set; }
    }
}
