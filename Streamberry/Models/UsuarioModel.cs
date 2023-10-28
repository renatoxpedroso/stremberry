using Streamberry.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Streamberry.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Senha { get; set; }

        public Enums.enPerfilUsuario Perfil { get; set; }

        public string PerfilString
        {
            get
            {
                if (this.Perfil == Enums.enPerfilUsuario.Administrador)
                {
                    return "Administrador";
                }
                else
                {
                    return "Usuário";
                }
            }
        }

        public UsuarioModel() 
        {
            Nome = string.Empty;
            Email = string.Empty;
            Senha = string.Empty;
        }
    }
}
