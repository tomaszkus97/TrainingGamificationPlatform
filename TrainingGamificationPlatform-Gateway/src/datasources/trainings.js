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

  async fillAttendance(groupId,date,attendantPlayers){
    const body = {
      groupId: groupId,
      date: date,
      attendantPlayers: attendantPlayers
    }
    const response = await this.post('Attendance',body);
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