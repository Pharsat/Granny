using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.Repository;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.DAO.EntitiesRepository
{
    public class LocationRepository : Repository<Location>, ILocationRepository
    {
        public LocationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task Create(Location location)
        {
            await ObjectSet.AddAsync(location);
        }

        public async Task<Location> GetByName(string location)
        {
            return await ObjectSet.Where(s => s.Name.ToLower().Equals(location.ToLower())).FirstOrDefaultAsync();
        }
    }
}
