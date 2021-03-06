module.exports = {
    Query: {
      players: (_, __, { dataSources }) =>
        dataSources.playersAPI.getPlayers(),
      player: (_, { id }, { dataSources }) =>
        dataSources.playersAPI.getPlayerById(id),
      coachSchedule: (_, { coachId }, { dataSources }) =>
        dataSources.trainingsAPI.getCoachSchedule(coachId),
      groups: (_, { ids }, { dataSources }) =>
        dataSources.trainingsAPI.getGroupsByIds(ids),
      coaches: (_,__,{ dataSources }) =>
        dataSources.trainingsAPI.getCoaches(),
      todayGroups: (_, __ , { dataSources }) =>
        dataSources.trainingsAPI.getTodayGroups(),
      challenges: (_, { ids }, { dataSources }) =>
        dataSources.gamificationAPI.getChallenges(ids)
    },
    Mutation: {
        login: async (_, { username,password }, { dataSources }) => {
          const token = await dataSources.identityAPI.login(username,password);
          return token
        },
        registerPlayer: async (_, {model}, { dataSources }) => {
          console.log("register player!");
          console.log(model);
          const response = await dataSources.identityAPI.registerPlayer(model);
          console.log(response);
        },
        registerCoach: async (_, {coach}, { dataSources }) => {
          console.log("register coach!");
          console.log(coach);
          const response = await dataSources.identityAPI.registerCoach(coach);
          console.log(response);
        },
        attendance: async (_, {groupId,date,attendantPlayers}, { dataSources }) => {
          const response = await dataSources.trainingsAPI.fillAttendance(groupId,date,attendantPlayers);
          console.log(response);
        },
        createGroup: async (_, {model}, {dataSources}) => {
          const response = await dataSources.trainingsAPI.createGroup(model);
          console.log(response);
        },
        assignGroup: async (_, {groupId, playerId}, {dataSources}) => {
          console.log("assign group!");
          const response = await dataSources.playersAPI.assignGroup(groupId, playerId);
          console.log(response);
        },
        createChallenge: async (_, {model}, {dataSources}) => {
          const response = await dataSources.gamificationAPI.createChallenge(model);
          console.log(response);
        }
  },
    Player: {
      groups: (player, { ids }, { dataSources }) =>
      {
        if(player.assignedGroups.length > 0){
         return dataSources.trainingsAPI.getGroupsByIds(player.assignedGroups)
        }
        else return [];
      }
  
    }

  }
