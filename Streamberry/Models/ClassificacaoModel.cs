using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Streamberry.Models
{
    [Table("Classificacao")]
    public class ClassificacaoModel
    {
        [Display(Name = "Código")]
        [Column("Id")]
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Comentario")]
        [Column("Comentario")]
        public string Comentario { get; set; }

        [Display(Name = "Nota")]
        [Column("Nota")]
        public int Nota { get; set; }

        //[Display(Name = "Código Usuário")]
        //[Column("IdUsuario")]
        //[ForeignKey("Usuario")]
        //public Guid IdUsuario { get; set; }

        //[Display(Name = "Usuario")]
        //[Column("Usuario")]
        //public UsuarioModel Usuario { get; set; }

        public ClassificacaoModel()
        {
            //Usuario = new UsuarioModel();
            Comentario = string.Empty;
        }
    }
}
