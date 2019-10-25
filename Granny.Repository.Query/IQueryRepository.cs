using Granny.DataModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Granny.Repository.Query
{
    public interface IQueryRepository
    {
        Task<Price> GetBestProductPrice(string pluCode);

        Task<IEnumerable<Price>> GetNextProductPrices(string pluCode, decimal value);

        Task<IEnumerable<Price>> GetPricesByLocation(string location);
    }
}
