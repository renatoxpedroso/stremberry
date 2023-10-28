using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Streamberry.Models
{
    public class StreamingFilmeModel
    {
        [Display(Name = "Código")]
        public Guid Id { get; set; }

        [Display(Name = "Filme")]
        public Guid Filme { get; set; }

        [Display(Name = "Streaming")]
        public Guid Streaming { get; set; }
    }
}
