
namespace Streamberry.Models
{
    public class GeneroModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public GeneroModel()
        {
            Nome = string.Empty;
        }
    }
}
