using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Trainings.Service.Contracts;
using Trainings.Service.Services.Clients;
using Xunit;

namespace TrainingsServiceTests.Clients
{
    public class PlayersServiceClientTests
    {
        [Fact]
        public async Task ShoulReturnPlayers()
        {
            var service = new PlayersServiceClient(new System.Net.Http.HttpClient(), Options.Create<PlayersServiceClientOptions>(
                new PlayersServiceClientOptions()
                {
                    BaseUrl = "https://localhost:44361/api/Players"
                }));

            var players = await service.GetPlayers(null);

            Assert.NotEmpty(players);
        }


    }
}
