using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Streamberry.Models;

namespace Streamberry.Data.Mapping
{
    public class StreamingFilmeMapping : IEntityTypeConfiguration<StreamingFilmeModel>
    {
        public void Configure(EntityTypeBuilder<StreamingFilmeModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Filme);
            builder.Property(x => x.Streaming);
        }
    }
}
