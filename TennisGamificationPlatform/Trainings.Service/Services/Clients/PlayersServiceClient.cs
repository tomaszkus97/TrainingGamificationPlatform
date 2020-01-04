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
            string ids = string.Empty;
            if (PlayerIds != null && PlayerIds.Any())
            {
                var strings = PlayerIds.Select(id => id.ToString());
                ids = "?Id=" + string.Join("&Id=", strings);
            }
            var httpResponse = await _client.GetAsync(_baseUrl + ids);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve players");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var players = JsonConvert.DeserializeObject<List<PlayerDto>>(content);

            return players;
        }
    }
}
