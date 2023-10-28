using Streamberry.Models;
using Streamberry.Models.Filtros;

namespace Streamberry.Repository.Interfaces
{
    public interface IFilmeRepository
    {
        Task<List<FilmeModel>> BuscarTodos();
        Task<FilmeModel> BuscarPorId(Guid id);
        List<FilmeModel> BuscaPorParametros(FiltroFilmeModel model);
        long ContarRegistroFiltroAsync(FiltroFilmeModel model);
        Task<FilmeModel> Adicionar(FilmeModel filme);
        Task<FilmeModel> Atualizar(FilmeModel filme, Guid id);
        Task<bool> Apagar(Guid id);
    }
}
