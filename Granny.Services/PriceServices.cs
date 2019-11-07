using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Granny.Services
{
    public class PriceServices : IPriceServices
    {
        private IUnitOfWork _unitOfWork;
        private ILocationServices _locationServices;
        private IProductServices _productServices;
        private IPriceRepository _priceRepository;

        public PriceServices(
            IUnitOfWork unitOfWork,
            IPriceRepository priceRepository,
            ILocationServices locationServices,
            IProductServices productServices)
        {
            this._unitOfWork = unitOfWork;
            this._priceRepository = priceRepository;
            this._locationServices = locationServices;
            this._productServices = productServices;
        }

        public async Task<int> Create(Price price)
        {
            await _priceRepository.AddAsync(price);
            await _unitOfWork.SaveAsync();
            return price.PriceId;
        }

        public async Task<Price> GetBestProductPrice(long pluCode)
        {
            Price price = await _priceRepository.GetBestProductPrice(pluCode);
            return price;
        }

        public async Task<IEnumerable<Price>> GetNextProductPrices(long pluCode, decimal value)
        {
            IEnumerable<Price> priceList = await _priceRepository.GetNextProductPrices(pluCode, value);
            return priceList;
        }

        public async Task<IEnumerable<Price>> GetPricesByLocation(string locationDescription)
        {
            IEnumerable<Price> priceList = await _priceRepository.GetPricesByLocation(locationDescription).ConfigureAwait(false);
            return priceList;
        }

        public async Task<int?> CheckIfExists(long productId, int locationId)
        {
            Price price = await _priceRepository.CheckIfExists(productId, locationId);
            return price?.PriceId;
        }
    }
}
