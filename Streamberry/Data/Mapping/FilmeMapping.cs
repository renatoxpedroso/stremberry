using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamberry.Models;

namespace Streamberry.Data.Mapping
{
    public class FilmeMapping : IEntityTypeConfiguration<FilmeModel>
    {
        public void Configure(EntityTypeBuilder<FilmeModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Titulo).IsRequired().HasMaxLength(120);
            builder.Property(x => x.Ano);
            builder.Property(x => x.IdGenero).IsRequired();
        }
    }
}
