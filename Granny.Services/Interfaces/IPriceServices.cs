using System.Collections.Generic;
using System.Threading.Tasks;
using Granny.DataModel;
using Granny.DataTransferObject.Price;

namespace Granny.Services.Interfaces
{
    public interface IPriceServices
    {
        Task<PriceDto> Create(PriceCreateDto price);

        Task<PriceDto> GetBestProductPrice(string pluCode);

        Task<IEnumerable<PriceDto>> GetNextProductPrices(string pluCode, decimal value);

        Task<IEnumerable<PriceDto>> GetPricesByLocation(string location);
    }
}
