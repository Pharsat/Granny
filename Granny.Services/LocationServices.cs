using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.Services
{
    public class LocationServices : ILocationServices
    {
        private ILocationRepository locationRepository;
        private IUnitOfWork unitOfWork;

        public LocationServices(IUnitOfWork unitOfWork, ILocationRepository locationRepository)
        {
            this.locationRepository = locationRepository;
            this.unitOfWork = unitOfWork;

        }

        public void Create(Location location)
        {
            if (location == null)
                throw new ArgumentNullException();

            this.locationRepository.Add(location);
            this.unitOfWork.Save();
        }

        public List<Location> Get()
        {
            List<Location> listLocations = this.locationRepository.getAll();
            return listLocations;
        }

        public Location GetByName(string location)
        {
            return this.locationRepository.GetByLocation(location);
        }

        public void Update(Location location)
        {
            if (location == null)
                throw new ArgumentNullException();

            this.locationRepository.Update(location);
            this.unitOfWork.Save();
        }
    }
}
