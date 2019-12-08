const { RESTDataSource } = require('apollo-datasource-rest');

class PlayersAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5001/api';
  }

  async getPlayers() {
    const response = await this.get('Players');
    return Array.isArray(response)
      ? response.map(player => this.playerReducer(player))
      : [];
  }

  async getPlayerById(playerId) {
    console.log(playerId);
    const response = await this.get(`Players/${playerId}`);
    return this.playerReducer(response);
  }

  playerReducer(player) {
    return {
      id: player.id || 0,
      name: `${player.name +' ' + player.surname}`,
      age: player.age,
      assignedGroups: player.assignedGroups,
      points: player.points,
      level: player.levelId
    };
  }
}

module.exports = PlayersAPI;