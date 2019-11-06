using System.Threading.Tasks;
using AutoMapper;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.DataTransferObject.Location;
using Granny.Services.Interfaces;

namespace Granny.Services
{
    public class LocationServices : ILocationServices
    {
        private ILocationRepository _locationRepository;
        private IUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public LocationServices(
            IUnitOfWork unitOfWork,
            ILocationRepository locationRepository, 
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(LocationDto locationDto)
        {
            Location location = _mapper.Map<Location>(locationDto);
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
