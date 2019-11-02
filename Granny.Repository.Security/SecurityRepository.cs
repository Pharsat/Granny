using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Repository.Security
{
    public class SecurityRepository : ISecurityRepository
    {
        public Task<int> CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUser(string email)
        {
            return Task.Run(() => new User() { UserId = 1, Email = "waruano9212@gmail.com", Name = "Alexis" });
        }
    }
}
