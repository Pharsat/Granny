using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System.Threading.Tasks;

namespace Granny.Services
{
    public class LocationServices : ILocationServices
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;

        public LocationServices(
            IUnitOfWork unitOfWork,
            ILocationRepository locationRepository)
        {
            _unitOfWork = unitOfWork;
            _locationRepository = locationRepository;
        }

        public async Task<int> Create(Location location)
        {
            await _locationRepository.AddAsync(location);
            await _unitOfWork.SaveAsync();
            return location.LocationId;
        }

        public async Task<Location> GetByName(string location)
        {
            return await _locationRepository.GetByName(location);
        }
    }
}
