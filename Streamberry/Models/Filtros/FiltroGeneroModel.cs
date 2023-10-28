namespace Streamberry.Models.Filtros
{
    public class FiltroGeneroModel
    {
        public string Nome { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public FiltroGeneroModel()
        {
            Nome = string.Empty;
        }
    }
}
