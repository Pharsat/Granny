using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Granny.Services
{
    public class PriceServices : IPriceServices
    {
        private IPriceRepository priceRepository;
        private IUnitOfWork unitOfWork;

        public PriceServices(IUnitOfWork unitOfWork, IPriceRepository priceRepository)
        {
            this.priceRepository = priceRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Add(price);
            this.unitOfWork.Save();
        }

        public List<Price> Get()
        {
            List<Price> listprice = this.priceRepository.getAll();
            return listprice;
        }

        public void Update(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Update(price);
            this.unitOfWork.Save();
        }
    }
}
