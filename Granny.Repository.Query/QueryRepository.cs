using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Granny.DataModel;

namespace Granny.Repository.Query
{
    public class QueryRepository : IQueryRepository
    {
        public Task<Price> GetBestProductPrice(string pluCode)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetNextProductPrices(string pluCode, decimal value)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Price>> GetPricesByLocation(string location)
        {
            throw new NotImplementedException();
        }
    }
}
