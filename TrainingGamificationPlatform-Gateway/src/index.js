const { ApolloServer, AuthenticationError } = require('apollo-server');
var jwt = require('jsonwebtoken');
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
      }),
    context: ({req}) =>{
      const secret = "TgpSecretWhichIsLonger";
      const token = req.headers.authorization || '';
      console.log(token);
      try {
        var decoded = jwt.verify(token, secret);
      } catch(err) {
        throw new AuthenticationError("You must be logged in!");
      }
      if(decoded.Role == ''){
        throw new AuthenticationError("You must be logged in!");
      }
      console.log(decoded);
    }
     });

server.listen().then(({ url }) => {
    console.log(`🚀 Server ready at ${url}`);
  });