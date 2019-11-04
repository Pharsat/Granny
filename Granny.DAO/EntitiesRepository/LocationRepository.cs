using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using System.Linq;

namespace Granny.DAO.EntitiesRepository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public Location GetByLocation(string location)
        {
            return this.ObjectSet.Where(s => s.Name.ToLower().Equals(location.ToLower())).FirstOrDefault();
        }
    }
}
