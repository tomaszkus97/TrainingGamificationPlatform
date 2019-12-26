const { RESTDataSource } = require('apollo-datasource-rest');

class IdentityAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5000/api/Identity';
  }

  async login(login , password){
      const body = {
          username: login,
          password: password
      }
      const response = await this.post('sign-in',body);
      return response
  }
  async registerPlayer(model){
    const body = {
      username: model.username,
      password: model.password,
      name: model.name,
      surname: model.surname,
      age: model.age,
      levelName: model.levelName
    }
    console.log(body);
    const response = await this.post('sign-up/player', body);
    return response;
  }
  async registerCoach(coach){
    const body = {
      username: coach.username,
      password: coach.password,
      name: coach.name,
      surname: coach.surname
    }
    const response = await this.post('sign-up/coach', body);
    return response;
  }
}

module.exports = IdentityAPI;