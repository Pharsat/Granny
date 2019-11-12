using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Services.Interfaces
{
    public interface IUserServices
    {
        Task<User> GetUser(string email);
        Task<string> CreateUser(User user);
    }
}
