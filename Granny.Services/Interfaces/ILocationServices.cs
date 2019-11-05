using System.Threading.Tasks;
using Granny.DataModel;
using Granny.DataTransferObject.Location;

namespace Granny.Services.Interfaces
{
    public interface ILocationServices
    {
        Task<int> Create(LocationDto location);

        Task<Location> GetByName(string location);
    }
}
