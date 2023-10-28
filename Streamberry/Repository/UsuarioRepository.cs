using Microsoft.EntityFrameworkCore;
using Streamberry.Data;
using Streamberry.Models;
using Streamberry.Models.Filtros;
using Streamberry.Repository.Interfaces;

namespace Streamberry.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly StreamberryDbContexto _dbContext;
        public UsuarioRepository(StreamberryDbContexto streamberryDBContext)
        {
            _dbContext = streamberryDBContext;
        }

        public async Task<List<UsuarioModel>> BuscarTodos()
        {
            return await _dbContext.Usuario.ToListAsync();
        }

        public UsuarioModel BuscarUsuarioLogin(string email, string senha)
        {
            return _dbContext.Usuario.FirstOrDefault(x => x.Email == email && x.Senha == senha);
        }

        public async Task<UsuarioModel> BuscarPorId(Guid id)
        {
            return await _dbContext.Usuario.FirstOrDefaultAsync(x => x.Id == id);
        }

        public List<UsuarioModel> BuscaPorParametros(FiltroUsuarioModel model)
        {
            IQueryable<UsuarioModel> query = _dbContext.Usuario.Skip(model.Start).Take(model.Limit);

            if (!string.IsNullOrEmpty(model.Nome))
            {
                query = query.Where(u => u.Nome == model.Nome);
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(u => u.Email == model.Email);
            }
              
            List<UsuarioModel> usuariosFiltrados = query.ToList();

            return usuariosFiltrados;
        }

        public long ContarRegistroFiltro(FiltroUsuarioModel model)
        {
            List<UsuarioModel> lista = new List<UsuarioModel>();

            lista = _dbContext.Usuario.ToList();

            if (!string.IsNullOrEmpty(model.Nome))
            {
                lista = _dbContext.Usuario.Where(p => p.Nome == model.Nome).ToList();
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                lista = _dbContext.Usuario.Where(p => p.Email == model.Email).ToList();
            }

            return lista.ToList().Count();
        }

        public bool ValidaUsuario(string email)
        {
            return (bool)(_dbContext.Usuario?.Any(e => e.Email == email));
        }

        public async Task<UsuarioModel> Adicionar(UsuarioModel usuario)
        {
            await _dbContext.AddAsync(usuario);
            _dbContext.SaveChanges();

            return usuario;
        }

        public async Task<UsuarioModel> Atualizar(UsuarioModel usuario, Guid id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário {usuario.Nome} não foi encontrado no banco de dados.");
            }

            usuarioPorId.Nome = usuario.Nome;
            usuarioPorId.Email = usuario.Email;
            usuarioPorId.Senha = usuario.Senha;
            usuarioPorId.Perfil = usuario.Perfil;

            _dbContext.Usuario.Update(usuarioPorId);
            _dbContext.SaveChanges();

            return usuarioPorId;
        }

        public async Task<bool> Apagar(Guid id)
        {
            UsuarioModel usuarioPorId = await BuscarPorId(id);

            if (usuarioPorId == null)
            {
                throw new Exception($"Usuário não foi encontrado no banco de dados.");
            }

            _dbContext.Usuario.Remove(usuarioPorId);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
