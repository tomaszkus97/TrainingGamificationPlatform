const { RESTDataSource } = require('apollo-datasource-rest');

class IdentityAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5001/api/Identity';
  }

  async login(login , password){
      const body = {
          username: login,
          password: password
      }
      const response = await this.post('sign-in',body);
      return response
  }
}

module.exports = IdentityAPI;