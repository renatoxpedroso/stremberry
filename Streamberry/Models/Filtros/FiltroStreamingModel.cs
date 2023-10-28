namespace Streamberry.Models.Filtros
{
    public class FiltroStreamingModel
    {
        public string Nome { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public FiltroStreamingModel()
        {
            Nome = string.Empty;
        }
    }
}
