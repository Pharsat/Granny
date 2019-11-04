using Granny.DataModel;
using System.Collections.Generic;

namespace Granny.Services.Interfaces
{
    public interface IPriceServices
    {
        void Create(Price price);

        void Update(Price price);

        List<Price> Get();
    }
}
