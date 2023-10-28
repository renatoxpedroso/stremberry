using Streamberry.Models;
using Streamberry.Models.Filtros;

namespace Streamberry.Repository.Interfaces
{
    public interface IGeneroRepository
    {
        Task<List<GeneroModel>> BuscarTodos();
        Task<GeneroModel> BuscarPorId(Guid id);
        List<GeneroModel> BuscaPorParametros(FiltroGeneroModel model);
        long ContarRegistroFiltro(FiltroGeneroModel model);
        Task<GeneroModel> Adicionar(GeneroModel genero);
        Task<GeneroModel> Atualizar(GeneroModel genero, Guid id);
        Task<bool> Apagar(Guid id);
    }
}
