const { gql } = require('apollo-server');

const typeDefs = gql`
type Query {
    players: [Player]!
    player(id: ID!): Player
    coachSchedule(coachId: String): [Schedule]
    groups(ids: [String]): [Group]
    coaches: [Coach]
}
  type Mutation {
  registerPlayer(model: RegisterPlayerModel): Response
  registerCoach(coach: RegisterCoachModel): Response
  login(username: String, password: String): String!
  attendance(groupId: String, date: String, attendantPlayers: [String]): Response
  createGroup(model: CreateGroupModel): Response
  assignGroup(groupId: String, playerId: String): Response
}
  type Player {
  id: String!
  name: String
  age: Int
  assignedGroups: [String]
  groups: [Group]
  points: Int
  level: Int
}
type Coach {
  id: String!
  name: String
}
type Schedule{
  day: String
  groups: [Group]
}
type Group{
  id: String
  name: String
  day: Int
  hour: String
  levelName: String
  players: [Player]
}
type User {
    id: String!
    name: String
    surname: String
    role: String
}
type Response{
  code: Int
}
input CreateGroupModel{
  day: String
  hour: String
  levelName: String
  coachId: String
}
input RegisterCoachModel{
  username: String
  password: String
  name: String
  surname: String
}
input RegisterPlayerModel{
  username: String
  password: String
  name: String
  surname: String
  age: Int
  levelName: String
}
`;

module.exports = typeDefs;