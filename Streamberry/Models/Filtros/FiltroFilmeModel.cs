namespace Streamberry.Models.Filtros
{
    public class FiltroFilmeModel
    {
        public string Titulo { get; set; }
        public int Ano { get; set; }
        public Guid IdGenero { get; set; }

        public int Start { get; set; }
        public int Limit { get; set; }

        public FiltroFilmeModel()
        {
            Titulo = string.Empty;
        }
    }
}
