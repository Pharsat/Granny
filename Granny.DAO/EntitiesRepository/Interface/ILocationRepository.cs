﻿using System.Threading.Tasks;
using Granny.DAO.Repository.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository.Interface
{
    public interface ILocationRepository : IRepository<Location>
    {
        Task<Location> GetByName(string location);
    }
}
