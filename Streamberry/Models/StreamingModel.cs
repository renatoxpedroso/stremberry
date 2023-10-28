using System.ComponentModel.DataAnnotations;

namespace Streamberry.Models
{
    public class StreamingModel
    {
        [Display(Name ="Código")]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        public StreamingModel()
        {
            Nome = string.Empty;        
        }
    }
}
