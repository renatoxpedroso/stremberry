using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamberry.Models;

namespace Streamberry.Data.Mapping
{
    public class GeneroMapping : IEntityTypeConfiguration<GeneroModel>
    {
        public void Configure(EntityTypeBuilder<GeneroModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
