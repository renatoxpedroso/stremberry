using Streamberry.Models.Filtros;
using Streamberry.Models;

namespace Streamberry.Repository.Interfaces
{
    public interface IStreamingFilmeRepository
    {
        Task<List<StreamingFilmeModel>> BuscarTodos();
        Task<List<StreamingFilmeModel>> BuscarPorIdFilme(Guid id);
        Task<StreamingFilmeModel> BuscarPorFilme(Guid id);
        Task<StreamingFilmeModel> Adicionar(StreamingFilmeModel vinculo);
        Task<bool> Apagar(Guid id);
    }
}
