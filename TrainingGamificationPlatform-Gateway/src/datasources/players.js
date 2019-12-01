const { RESTDataSource } = require('apollo-datasource-rest');

class PlayersAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5003/api';
  }

  async getPlayers() {
    const response = await this.get('Players');
    return Array.isArray(response)
      ? response.map(player => this.playerReducer(player))
      : [];
  }

  async getPlayerById({ playerId }) {
    const response = await this.get({ playerId: playerId });
    return this.playerReducer(response[0]);
  }

  playerReducer(player) {
    return {
      id: player.id || 0,
      name: `${player.name +' ' + player.surname}`,
      age: player.age,
      assignedGroups: player.assignedGroups
    };
  }
}

module.exports = PlayersAPI;