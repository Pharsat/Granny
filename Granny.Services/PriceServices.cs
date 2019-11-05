using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System;

namespace Granny.Services
{
    public class PriceServices : IPriceServices
    {
        private ILocationServices locationServices;
        private IProductServices productServices;
        private IPriceRepository priceRepository;
        private IUnitOfWork unitOfWork;

        public PriceServices(
            IUnitOfWork unitOfWork, 
            IPriceRepository priceRepository,
            ILocationServices locationServices,
            IProductServices productServices)
        {
            this.priceRepository = priceRepository;
            this.locationServices = locationServices;
            this.productServices = productServices;
            this.unitOfWork = unitOfWork;
        }

        public void Create(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Add(price);
            this.unitOfWork.Save();
        }
        public void Update(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Update(price);
            this.unitOfWork.Save();
        }

        public Price GetByProduct(long productId)
        {
            return this.priceRepository.GetByProduct(productId);
        }

        public Price GetByProductLocation(long productId, int locationId)
        {
            return this.priceRepository.GetByProductLocation(productId, locationId);
        }
    }
}
