using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Streamberry.Data.Mapping;
using Streamberry.Models;

namespace Streamberry.Data
{
    public class StreamberryDbContexto : DbContext
    {
        public StreamberryDbContexto(DbContextOptions<StreamberryDbContexto> options) : base(options) 
        { 
        }

        public DbSet<UsuarioModel> Usuario { get; set; }
        public DbSet<GeneroModel> Genero { get; set; }
        public DbSet<StreamingModel> Streaming { get; set; }
        public DbSet<FilmeModel> Filme { get; set; }
        public DbSet<StreamingFilmeModel> StreaminfFilme { get; set; }


        public DbSet<ClassificacaoModel> Classificacao { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMapping());
            modelBuilder.ApplyConfiguration(new GeneroMapping());
            modelBuilder.ApplyConfiguration(new StreamingMapping());
            modelBuilder.ApplyConfiguration(new FilmeMapping());
            modelBuilder.ApplyConfiguration(new StreamingFilmeMapping());

            base.OnModelCreating(modelBuilder);
        }
    }
}
