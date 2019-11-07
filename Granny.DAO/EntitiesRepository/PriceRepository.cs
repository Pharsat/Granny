using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.DAO.EntitiesRepository
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        public PriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Price> CheckIfExists(long productId, int locationId)
        {
            return await ObjectSet.Where(p => p.ProductId == productId && p.LocationId == locationId).FirstOrDefaultAsync();
        }

        //public async Task Create(Price price)
        //{
        //    await ObjectSet.AddAsync(price);
        //}

        public async Task<Price> GetBestProductPrice(long productId)
        {
            return await ObjectSet.Where(s => s.ProductId.Equals(productId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Price>> GetNextProductPrices(long productId, decimal value)
        {
            return await (from price in ObjectSet
                          where price.ProductId.Equals(productId)
                          && price.Value > value
                          orderby price.Value 
                          select price).ToListAsync();
        }

        public async Task<IEnumerable<Price>> GetPricesByLocation(string locationDescription)
        {
            return await (from price in ObjectSet
                          where price.Location.Name.Equals(locationDescription)
                          orderby price.Value 
                          select price).ToListAsync();
        }
}
}
