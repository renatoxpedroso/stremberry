using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository.Interfaces;

namespace Streamberry.Repository
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly StreamberryDbContexto _dbContext;
        public GeneroRepository(StreamberryDbContexto streamberryDbContexto)
        {
            _dbContext = streamberryDbContexto;
        }

        public async Task<GeneroModel> BuscarPorId(Guid id)
        {
           return await _dbContext.Genero.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<GeneroModel>> BuscarTodos()
        {
            return await _dbContext.Genero.ToListAsync();
        }

        public List<GeneroModel> BuscaPorParametros(FiltroGeneroModel model)
        {
            IQueryable<GeneroModel> query = _dbContext.Genero.Skip(model.Start).Take(model.Limit);

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query = query.Where(u => u.Nome == model.Nome);
            }

            List<GeneroModel> generosFiltrados = query.ToList();

            return generosFiltrados;
        }

        public long ContarRegistroFiltro(FiltroGeneroModel model)
        {
            List<GeneroModel> lista = new List<GeneroModel>();

            lista = _dbContext.Genero.ToList();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                lista = _dbContext.Genero.Where(p => p.Nome == model.Nome).ToList();
            }

            return lista.ToList().Count();
        }

        public async Task<GeneroModel> Adicionar(GeneroModel genero)
        {
            await _dbContext.AddAsync(genero);
            _dbContext.SaveChanges();

            return genero;
        }

        public async Task<GeneroModel> Atualizar(GeneroModel genero, Guid id)
        {
            GeneroModel generoPorId = await BuscarPorId(id);

            if (generoPorId == null)
            {
                throw new Exception($"Gênero {genero.Nome} não foi encontrado no banco de dados.");
            }

            generoPorId.Nome = genero.Nome;

            _dbContext.Genero.Update(generoPorId);
            _dbContext.SaveChanges();

            return generoPorId;
        }

        public async Task<bool> Apagar(Guid id)
        {
            GeneroModel generoPorId = await BuscarPorId(id);

            if (generoPorId == null)
            {
                throw new Exception($"Usuário não foi encontrado no banco de dados.");
            }

            _dbContext.Genero.Remove(generoPorId);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
