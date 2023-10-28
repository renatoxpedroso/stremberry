using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Streamberry.Enum
{
    public class Enums
    {
        public enum enPerfilUsuario
        {
            [Description("Administrador")]
            [Display(Name = "Administrador")]
            Administrador = 0,
            [Description("Usuário")]
            [Display(Name = "Usuario")]
            Usuario = 1,
        }
    }
}
