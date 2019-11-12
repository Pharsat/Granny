using System.Threading.Tasks;
using Granny.DataModel;
using Granny.Repository.Security;
using Granny.Services.Interfaces;

namespace Granny.Services
{
    public class UserServices : IUserServices
    {
        private IUserRepository _userRepository;

        public UserServices(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<User> GetUser(string email)
        {
            return await _userRepository.GetUser(email);
        }
    }
}
