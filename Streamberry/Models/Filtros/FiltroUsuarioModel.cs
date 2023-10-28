using System.Security.Policy;

namespace Streamberry.Models.Filtros
{
    public class FiltroUsuarioModel
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public int Start { get; set; }
        public int Limit { get; set; }
        public FiltroUsuarioModel()
        {
            Nome = string.Empty;
            Email = string.Empty;   
        }
    }
}
