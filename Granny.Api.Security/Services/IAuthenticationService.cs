using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Api.Security.Services
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUser> Authenticate(string email);
    }
}
