using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using System.Linq;

namespace Granny.DAO.EntitiesRepository
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        public PriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Price GetByProduct(long productId)
        {
            return this.ObjectSet.Where(s => s.PluCode.Equals(productId)).FirstOrDefault();
        }

        public Price GetByProductLocation(long productId, int locationId)
        {
            return this.ObjectSet.Where(s => s.PluCode.Equals(productId) && s.LocationId == locationId).FirstOrDefault();
        }
    }
}
