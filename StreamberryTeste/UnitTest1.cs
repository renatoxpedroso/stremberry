using Streamberry.Models;
using System.Net;
using System.Net.Http.Json;
using System.Security.Policy;

namespace StreamberryTeste
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Criar_Genero()
        {
            await using var application = new StreamberryAplication();

            await CatalogoMockData.CreateGeneros(application, true);
        }
    }
}