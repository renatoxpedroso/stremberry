using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository.Interfaces;

namespace Streamberry.Repository
{
    public class StreamingRepository : IStreamingRepository
    {
        private readonly StreamberryDbContexto _dbContext;
        public StreamingRepository(StreamberryDbContexto streamberryDbContexto)
        {
            _dbContext = streamberryDbContexto;
        }

        public async Task<StreamingModel> BuscarPorId(Guid id)
        {
            return await _dbContext.Streaming.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<StreamingModel>> BuscarTodos()
        {
            return await _dbContext.Streaming.ToListAsync();
        }

        public List<StreamingModel> BuscaPorParametros(FiltroStreamingModel model)
        {
            IQueryable<StreamingModel> query = _dbContext.Streaming.Skip(model.Start).Take(model.Limit);

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query =  query.Where(u => u.Nome == model.Nome);
            }

            List<StreamingModel> streamingsFiltrados = query.ToList();

            return streamingsFiltrados;
        }

        public long ContarRegistroFiltro(FiltroStreamingModel model)
        {
            List<StreamingModel> lista = new List<StreamingModel>();

            lista = _dbContext.Streaming.ToList();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                lista = _dbContext.Streaming.Where(p => p.Nome == model.Nome).ToList();
            }

            return lista.ToList().Count();
        }

        public async Task<StreamingModel> Adicionar(StreamingModel streaming)
        {
            await _dbContext.AddAsync(streaming);
            _dbContext.SaveChanges();

            return streaming;
        }

        public async Task<StreamingModel> Atualizar(StreamingModel streaming, Guid id)
        {
            StreamingModel streamingPorId = await BuscarPorId(id);

            if (streamingPorId == null)
            {
                throw new Exception($"Streaming {streaming.Nome} não foi encontrado no banco de dados.");
            }

            streamingPorId.Nome = streaming.Nome;

            _dbContext.Streaming.Update(streamingPorId);
            _dbContext.SaveChanges();

            return streamingPorId;
        }

        public async Task<bool> Apagar(Guid id)
        {
            StreamingModel streamingPorId = await BuscarPorId(id);

            if (streamingPorId == null)
            {
                throw new Exception($"Streaming não foi encontrado no banco de dados.");
            }

            _dbContext.Streaming.Remove(streamingPorId);
            _dbContext.SaveChanges();
            return true;
        }

    }
}
