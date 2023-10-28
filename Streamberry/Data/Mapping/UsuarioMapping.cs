using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamberry.Models;

namespace Streamberry.Data.Mapping
{
    public class UsuarioMapping : IEntityTypeConfiguration<UsuarioModel>
    {
        public void Configure(EntityTypeBuilder<UsuarioModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Senha).IsRequired();
            builder.Property(x => x.Perfil);
        }
    }
}
