module.exports = {
    Query: {
      players: (_, __, { dataSources }) =>
        dataSources.playersAPI.getPlayers(),
      player: (_, { id }, { dataSources }) =>
      {
        dataSources.playersAPI.getPlayerById(id)
      },
      coachSchedule: (_, { coachId }, { dataSources }) =>
        dataSources.trainingsAPI.getCoachSchedule(coachId)
    },
    Mutation: {
        login: async (_, { username,password }, { dataSources }) => {
          const token = await dataSources.identityAPI.login(username,password);
          return token
        },
        attendance: async (_, {groupId,date,attendantPlayers}, { dataSources }) => {
          const response = await dataSources.trainingsAPI.fillAttendance(groupId,date,attendantPlayers);
          console.log(response);
        },
        createGroup: async (_, {model}, {dataSources}) => {
          const response = await dataSources.trainingsAPI.createGroup(model);
          console.log(response);
        }
      },
}