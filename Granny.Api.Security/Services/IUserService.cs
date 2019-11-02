using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Api.Security.Services
{
    public interface IUserService
    {
        Task<AuthenticatedUser> Authenticate(string email);
    }
}
