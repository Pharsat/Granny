using Granny.DataModel;
using System;
using System.Threading.Tasks;

namespace Granny.Repository.Security
{
    public interface IUserRepository
    {
        Task<User> GetUser(string email);
        Task<string> CreateUser(User user);
    }
}
