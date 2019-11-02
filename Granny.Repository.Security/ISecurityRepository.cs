using Granny.DataModel;
using System;
using System.Threading.Tasks;

namespace Granny.Repository.Security
{
    public interface ISecurityRepository
    {
        Task<User> GetUser(string email);
        Task<int> CreateUser(User user);
    }
}
