using Streamberry.Models;
using Streamberry.Models.Filtros;

namespace Streamberry.Repository.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<List<UsuarioModel>> BuscarTodos();
        Task<UsuarioModel> BuscarPorId(Guid id);
        UsuarioModel BuscarUsuarioLogin(string email, string senha);
        List<UsuarioModel> BuscaPorParametros(FiltroUsuarioModel model);
        long ContarRegistroFiltro(FiltroUsuarioModel model);
        bool ValidaUsuario(string email);
        Task<UsuarioModel> Adicionar(UsuarioModel usuario);
        Task<UsuarioModel> Atualizar(UsuarioModel usuario, Guid id);
        Task<bool> Apagar(Guid id);
    }
}
