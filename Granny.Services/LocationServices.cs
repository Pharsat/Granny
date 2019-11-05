using System.Threading.Tasks;
using AutoMapper;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DataModel;
using Granny.DataTransferObject.Location;
using Granny.Services.Interfaces;

namespace Granny.Services
{
    public class LocationServices : ILocationServices
    {
        private ILocationRepository _locationRepository;
        private IMapper _mapper;

        public LocationServices(
            ILocationRepository locationRepository, 
            IMapper mapper)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(LocationDto locationDto)
        {
            Location location = _mapper.Map<Location>(locationDto);
            await _locationRepository.AddAsync(location);
            return location.LocationId;
        }

        public async Task<Location> GetByName(string location)
        {
            return await _locationRepository.GetByName(location);
        }
    }
}
