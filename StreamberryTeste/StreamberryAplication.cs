using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Streamberry.Data;

namespace StreamberryTeste
{
    public class StreamberryAplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<StreamberryDbContexto>));
                services.AddDbContext<StreamberryDbContexto>(options => options.UseInMemoryDatabase("streamberry", root));
            });

            return base.CreateHost(builder);
        }
    }
}
