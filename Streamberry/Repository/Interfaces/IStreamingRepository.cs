using Streamberry.Models;
using Streamberry.Models.Filtros;

namespace Streamberry.Repository.Interfaces
{
    public interface IStreamingRepository
    {
        Task<List<StreamingModel>> BuscarTodos();
        Task<StreamingModel> BuscarPorId(Guid id);
        List<StreamingModel> BuscaPorParametros(FiltroStreamingModel model);
        long ContarRegistroFiltro(FiltroStreamingModel model);
        Task<StreamingModel> Adicionar(StreamingModel streaming);
        Task<StreamingModel> Atualizar(StreamingModel streaming, Guid id);
        Task<bool> Apagar(Guid id);
    }
}
