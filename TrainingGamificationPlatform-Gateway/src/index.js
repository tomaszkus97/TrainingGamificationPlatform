const { ApolloServer } = require('apollo-server');
const typeDefs = require('./schemas/schema');
const resolvers = require('./resolvers/resolvers');
const PlayersAPI = require('./datasources/players');
const IdentityAPI =  require('./datasources/identity');
const TrainingsAPI =  require('./datasources/trainings');



const server = new ApolloServer({ 
    typeDefs,
    resolvers,
    dataSources: () => ({
        playersAPI: new PlayersAPI(),
        identityAPI: new IdentityAPI(),
        trainingsAPI: new TrainingsAPI()
      })
     });

server.listen().then(({ url }) => {
    console.log(`🚀 Server ready at ${url}`);
  });