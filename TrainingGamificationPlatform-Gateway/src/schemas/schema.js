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
  login(username: String, password: String): String!
  attendance(groupId: String, date: String, attendantPlayers: [String]): Response
  createGroup(model: CreateGroupModel): Response
}
  type Player {
  id: String!
  name: String
  age: Int
  assignedGroups: [String]
  groups: [Group]
  points: Int
  level: String
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
}
`;

module.exports = typeDefs;