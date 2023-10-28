using Microsoft.Extensions.DependencyInjection;
using Streamberry.Data;
using Streamberry.Models;

namespace StreamberryTeste
{
    public class CatalogoMockData
    {
        public static async Task CreateGeneros(StreamberryAplication application, bool criar)
        {
            using (var scope = application.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                using (var streamberryDbContext = provider.GetRequiredService<StreamberryDbContexto>())
                {
                    await streamberryDbContext.Database.EnsureCreatedAsync();

                    if (criar)
                    {
                        await streamberryDbContext.Genero.AddAsync(new GeneroModel
                        {
                            Id = Guid.NewGuid(),
                            Nome = "Ação"
                        });


                        await streamberryDbContext.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
