using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Streamberry.Repository
{
    public class FilmeRepository : IFilmeRepository
    {
        private readonly StreamberryDbContexto _dbContext;

        public FilmeRepository(StreamberryDbContexto streamberryDbContexto)
        {
            _dbContext = streamberryDbContexto;
        }

        public async Task<List<FilmeModel>> BuscarTodos()
        {
            return await _dbContext.Filme.ToListAsync();
        }

        public async Task<FilmeModel> BuscarPorId(Guid id)
        {
            return await _dbContext.Filme.FirstOrDefaultAsync(x => x.Id == id); 
        }

        public List<FilmeModel> BuscaPorParametros(FiltroFilmeModel model)
        {
            IQueryable<FilmeModel> query = _dbContext.Filme;

            if (!string.IsNullOrEmpty(model.Titulo))
            {
                query = query.Where(p => p.Titulo == model.Titulo);
            }

            if (model.Ano > 0)
            {
                query = query.Where(u => u.Ano == model.Ano);
            }

            if (model.IdGenero != Guid.Empty)
            {
                query = query.Where(u => u.IdGenero == model.IdGenero);
            }


            List<FilmeModel> filmesFiltrados = query.Skip(model.Start).Take(model.Limit).ToList();

            for(int i =0; i < filmesFiltrados.Count; i++)
            {
                filmesFiltrados[i].Genero = _dbContext.Genero.FirstOrDefault(g => g.Id == filmesFiltrados[i].IdGenero);
            }

            return filmesFiltrados;
        }

        public long ContarRegistroFiltroAsync(FiltroFilmeModel model)
        {
            IQueryable<FilmeModel> query = _dbContext.Filme;

            if (!string.IsNullOrEmpty(model.Titulo))
            {
                query = query.Where(p => p.Titulo == model.Titulo);
            }

            if (model.Ano > 0)
            {
                query = query.Where(u => u.Ano == model.Ano);
            }

            if (model.IdGenero != Guid.Empty)
            {
                query = query.Where(u => u.IdGenero == model.IdGenero);
            }

            List<FilmeModel> filmesFiltrados = query.ToList();

            return filmesFiltrados.Count();
        }

        public async Task<FilmeModel> Adicionar(FilmeModel filme)
        {
            await _dbContext.AddAsync(filme);
            _dbContext.SaveChanges();

            return filme;
        }

        public async Task<FilmeModel> Atualizar(FilmeModel filme, Guid id)
        {
            FilmeModel filmePorId = await BuscarPorId(id);
           
            if (filmePorId == null)
            {
                throw new Exception($"Filme {filme.Titulo} não foi encontrado no banco de dados.");
            }

            filmePorId.Titulo = filme.Titulo;
            filmePorId.Ano = filme.Ano;
            filmePorId.IdGenero = filme.IdGenero;
            filmePorId.Genero = filme.Genero;

            _dbContext.Filme.Update(filmePorId);
            _dbContext.SaveChanges();

            return filmePorId;
        }

        public async Task<bool> Apagar(Guid id)
        {
            FilmeModel filmePorId = await BuscarPorId(id);

            if (filmePorId == null)
            {
                throw new Exception($"Streaming não foi encontrado no banco de dados.");
            }

            _dbContext.Filme.Remove(filmePorId);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
