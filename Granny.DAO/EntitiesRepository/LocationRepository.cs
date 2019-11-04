using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
