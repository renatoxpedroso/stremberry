using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamberry.Models;

namespace Streamberry.Data.Mapping
{
    public class StreamingMapping : IEntityTypeConfiguration<StreamingModel>
    {
        public void Configure(EntityTypeBuilder<StreamingModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(120);
        }
    }
}
