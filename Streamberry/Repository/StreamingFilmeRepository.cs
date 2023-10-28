using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Repository.Interfaces;

namespace Streamberry.Repository
{
    public class StreamingFilmeRepository : IStreamingFilmeRepository
    {
        private readonly StreamberryDbContexto _dbContext;
        public StreamingFilmeRepository(StreamberryDbContexto streamberryDBContext)
        {
            _dbContext = streamberryDBContext;
        }

        public async Task<List<StreamingFilmeModel>> BuscarPorIdFilme(Guid id)
        {
            IQueryable<StreamingFilmeModel> query = _dbContext.StreaminfFilme;
            query = query.Where(x => x.Filme == id);

            List<StreamingFilmeModel> vinculos = query.ToList();

            return vinculos;
        }

        public async Task<StreamingFilmeModel> BuscarPorFilme(Guid id)
        {
 
            return await _dbContext.StreaminfFilme.FirstOrDefaultAsync(x => x.Filme == id);
        }

        public async Task<List<StreamingFilmeModel>> BuscarTodos()
        {
            return await _dbContext.StreaminfFilme.ToListAsync();
        }

        public async Task<StreamingFilmeModel> Adicionar(StreamingFilmeModel vinculo)
        {
            await _dbContext.AddAsync(vinculo);
            _dbContext.SaveChanges();

            return vinculo;
        }

        public async Task<bool> Apagar(Guid id)
        {
            StreamingFilmeModel vinculoPorIdFilme = await BuscarPorFilme(id);

            if (vinculoPorIdFilme != null)
            {
                _dbContext.StreaminfFilme.Remove(vinculoPorIdFilme);
                _dbContext.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
