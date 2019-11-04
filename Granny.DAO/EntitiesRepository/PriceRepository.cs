using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository
{
    public class PriceRepository : Repository<Price>, IPriceRepository
    {
        public PriceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
