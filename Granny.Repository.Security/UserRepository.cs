using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Granny.DataModel;
using Granny.Repository.Security.Mongo;
using MongoDB.Driver;

namespace Granny.Repository.Security
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;


        public UserRepository(IGrannySecurityDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public async Task<string> CreateUser(User user)
        {
            await _users.InsertOneAsync(user).ConfigureAwait(false);
            return user.UserId;
        }

        public async Task<User> GetUser(string email)
        {
            return await _users.Find(user => user.Email == email).FirstOrDefaultAsync().ConfigureAwait(false);
        }
    }
}
