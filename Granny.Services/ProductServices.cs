using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Services.Interfaces;
using System;

namespace Granny.Services
{
    public class ProductServices : IProductServices
    {
        private IProductRepository productRepository;
        private IUnitOfWork unitOfWork;

        public ProductServices(
            IUnitOfWork unitOfWork, 
            IProductRepository productRepository, 
            ILocationServices locationServices, 
            IPriceServices priceServices)
        {
            this.productRepository = productRepository;
            this.unitOfWork = unitOfWork;
        }

        public void Create(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            this.productRepository.Add(product);
            this.unitOfWork.Save();
        }

        public Product GetById(long productId)
        {
            return this.productRepository.Get(productId);
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException();

            this.productRepository.Update(product);
            this.unitOfWork.Save();
        }
    }
}
