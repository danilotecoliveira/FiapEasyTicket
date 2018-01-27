namespace FiapEasyTicket.Models
{
    public class Filme
    {
        public const int _PIPOCA = 20;
        public const int _CHOCOLATE = 10;
        public const int _REFRIGERANTE = 10;

        public string Cartaz { get; set; }
        public string Titulo { get; set; }
        public decimal Preco { get; set; }
        public string PrecoFormatado
        {
            get { return string.Format("R$ {0}", Preco); }
        }

        public bool TemPipoca { get; set; }
        public bool TemChocolate { get; set; }
        public bool TemRefrigerante { get; set; }

        public string PrecoTotalFormatado
        {
            get
            {
                return string.Format("Valor total: R$ {0}",
                    Preco +
                    ((TemPipoca) ? _PIPOCA : 0) +
                    ((TemChocolate) ? _CHOCOLATE : 0) +
                    ((TemRefrigerante) ? _REFRIGERANTE : 0));
            }
        }
    }
}
