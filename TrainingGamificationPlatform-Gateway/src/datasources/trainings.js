const { RESTDataSource } = require('apollo-datasource-rest');

class TrainingsAPI extends RESTDataSource {
  constructor() {
    super();
    this.baseURL = 'http://localhost:5002/api';
  }

  async getCoachSchedule(coachId) {
    const response = await this.get(`Schedule/coach/${coachId}`);
    return Array.isArray(response)
      ? response.map(day => this.scheduleReducer(day))
      : [];
  }

  async getCoaches() {
    const response = await this.get(`Groups/Coaches`);
    return Array.isArray(response)
      ? response.map(day => this.coachReducer(day))
      : [];
  }

  async getGroupsByIds(ids) {
    var params = new URLSearchParams();
    if(ids){
    ids.map(id => params.append('ids',id));
    }
    console.log(params);
    const response = await this.get(`Groups`,params);
    return Array.isArray(response)
      ? response.map(group => this.groupsReducer(group))
      : [];
  }


  async fillAttendance(groupId,date,attendantPlayers){
    const body = {
      groupId: groupId,
      date: date,
      attendantPlayers: attendantPlayers
    }
    const response = await this.post('Attendance',body);
    return response;
  }

  async createGroup(model){
    const body ={
      day: model.day,
      hour: model.hour,
      levelName: model.levelName
    }
    console.log(body);
    const response = await this.post('Groups', body);
    return response;
  }

  scheduleReducer(day){
    return {
      day: day.day,
      groups: day.trainings.map(training => this.trainingsReducer(training))
    }
  }

  playerReducer(player) {
    return {
      id: player.id || 0,
      name: `${player.name +' ' + player.surname}`,
      age: player.age,
      assignedGroups: player.assignedGroups
    };
  }
  coachReducer(coach) {
    return {
      id: coach.id || 0,
      name: `${coach.name +' ' + coach.surname}`,
    };
  }
  groupsReducer(group){
    return{
      name: group.groupName,
      day: group.day,
      hour: group.hour,
      levelName: group.levelName,
      players: null
    }
  }

  trainingsReducer(training){
    return{
      name: training.groupName,
      day: training.day,
      hour: training.hour,
      levelName: training.levelName,
      players: training.players.map(player => this.playerReducer(player))
    }
  }

  
}

module.exports = TrainingsAPI;