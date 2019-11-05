﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Granny.DAO.Repository.Interface;
using Granny.DataModel;

namespace Granny.DAO.EntitiesRepository.Interface
{
    public interface IPriceRepository : IRepository<Price>
    {
        Task<bool> CheckIfExists(long productId, decimal value, int locationId);

        Task<Price> GetBestProductPrice(long productId);

        Task<IEnumerable<Price>> GetNextProductPrices(long productId, decimal value);

        Task<IEnumerable<Price>> GetPricesByLocation(string locationDescription);

        Task Create(Price price);
    }
}
