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

        private readonly DbSet<Location> _locationSet;

        public PriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _locationSet = unitOfWork.grannyContext.Set<Location>();
        }

        public async Task<bool> CheckIfExists(long productId, decimal value, int locationId)
        {
            return await ObjectSet.AnyAsync(p => p.ProductId == productId && p.Value == value && p.LocationId == locationId);
        }

        public async Task Create(Price price)
        {
            await ObjectSet.AddAsync(price);
        }

        public async Task<Price> GetBestProductPrice(long productId)
        {
            return await ObjectSet.Where(s => s.ProductId.Equals(productId)).OrderByDescending(p => p.Value).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Price>> GetNextProductPrices(long productId, decimal value)
        {
            return await (from price in ObjectSet
                          where price.ProductId.Equals(productId)
                          && price.Value > value
                          orderby price.Value descending
                          select price).ToListAsync();
        }

        public async Task<IEnumerable<Price>> GetPricesByLocation(string locationDescription)
        {
            return await (from price in ObjectSet
                          join location in _locationSet on price.LocationId equals location.LocationId
                          where location.Name.Equals(locationDescription)
                          orderby price.Value descending
                          select price).ToListAsync();
        }
    }
}
