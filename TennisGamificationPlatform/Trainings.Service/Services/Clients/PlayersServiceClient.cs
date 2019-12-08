using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Trainings.Service.Contracts;
using Trainings.Service.Dtos;

namespace Trainings.Service.Services.Clients
{
    public class PlayersServiceClient : IPlayersServiceClient
    {
        private readonly string _baseUrl;
        private readonly HttpClient _client;

        public PlayersServiceClient(HttpClient client, IOptions<PlayersServiceClientOptions> settings)
        {
            _client = client;
            _baseUrl = "http://players.service:5000/api/Players";
        }

        public async Task<IEnumerable<PlayerDto>> GetPlayers(IEnumerable<Guid> PlayerIds)
        {
            var httpResponse = await _client.GetAsync(_baseUrl);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve tasks");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<PlayerDto>>(content);

            return players;
        }
    }
}
