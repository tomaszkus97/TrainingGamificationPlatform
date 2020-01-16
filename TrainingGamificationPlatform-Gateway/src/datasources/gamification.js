const { RESTDataSource } = require('apollo-datasource-rest');

class GamificationAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5003/api';
}

  async getChallenges(ids) {
    var params = new URLSearchParams();
    if(ids){
    ids.map(id => params.append('ids',id));
    }
    const response = await this.get('Challenges',params);
    return Array.isArray(response)
      ? response.map(player => this.challengeReducer(player))
      : [];
  }

  async createChallenge(model){
    const body ={
      title: model.title,
      description: model.description,
      levelId: model.level,
      isObligatory: model.isObligatory
    }
    const response = await this.post('Challenges', body);
    Array.isArray(response)
      ? response.map(challenge => console.log(challenge))
      : console.log(response);
    return response;
  }

  challengeReducer(challenge) {
    return {
      id: challenge.id || 0,
      title: challenge.title,
      description: challenge.description,
      isObligatory: challenge.isObligatory,
      level: challenge.levelId
    };
  }
}
module.exports = GamificationAPI;