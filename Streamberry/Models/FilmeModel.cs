using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamberry.Models
{
    public class FilmeModel
    {
        [Display(Name = "Código")]
        public Guid Id { get; set; }

        [Display(Name = "Titulo")]
        public string Titulo { get; set; }

        [Display(Name = "Ano")]
        public int Ano { get; set; }

        [Display(Name = "Genero")]
        [Column("IdGenero")]
        [ForeignKey("Genero")]
        public Guid IdGenero { get; set; }

        [Display(Name = "Gênero")]
        [Column("Genero")]
        public GeneroModel Genero { get; set; }

        [Display(Name = "Streamings")]
        public List<StreamingModel> Streamings { get; set; }

        public FilmeModel()
        {
            Titulo = string.Empty;
            Genero = new GeneroModel();
            Streamings = new List<StreamingModel>();
        }
    }
}
