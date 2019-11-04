using Granny.DataModel;
using System.Collections.Generic;

namespace Granny.Services.Interfaces
{
    public interface ILocationServices
    {
        void Create(Location location);

        void Update(Location location);

        List<Location> Get();
    }
}
