using System;
using System.Collections.Generic;
using System.Linq;
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
            var service = new PlayersServiceClient(new System.Net.Http.HttpClient(),
                Options.Create<PlayersServiceClientOptions>(
                    new PlayersServiceClientOptions()
                    {
                        BaseUrl = "http://localhost:5001/api/Players"
                    }));

            var players = await service.GetPlayers(null);

            Assert.NotEmpty(players);
        }

        [Fact]
        public async Task ShoulReturnRequestedPlayers()
        {
            var service = new PlayersServiceClient(new System.Net.Http.HttpClient(), Options.Create<PlayersServiceClientOptions>(
                new PlayersServiceClientOptions()
                {
                    BaseUrl = "http://localhost:5001/api/Players"
                }));

            var players = await service.GetPlayers(new List<Guid> {
                new Guid("4F840DC9-F742-4170-94B5-08D78A07DA27")
            });

            Assert.NotEmpty(players);
            Assert.Equal(1, players.Count());
        }


    }
}

