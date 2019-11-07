using Granny.DataModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Granny.Services.Interfaces
{
    public interface IPriceServices
    {
        Task<int> Create(Price price);

        Task<Price> GetBestProductPrice(long pluCode);

        Task<IEnumerable<Price>> GetNextProductPrices(long pluCode, decimal value);

        Task<IEnumerable<Price>> GetPricesByLocation(string location);

        Task<int?> CheckIfExists(long productId, int locationId);
    }
}
