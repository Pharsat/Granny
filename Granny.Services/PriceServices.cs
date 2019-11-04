using Granny.Api.Register.Model;
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

        public void Register(ProductCreateDto productCreateDto)
        {
            Location location = this.ValidateLocation(productCreateDto);
            Product product = this.ValidateProduct(productCreateDto);
            Price price = this.ValidatePrice(product.ProductId, location.LocationId, productCreateDto.Price);

            if (price.PriceId == 0)
            {
                this.Create(price);
            }
            else
            {
                this.Update(price);
            }

            this.unitOfWork.Save();
        }

        private Price ValidatePrice(long productId, int locationId, decimal value)
        {
            Price price = this.priceRepository.GetByProductLocation(productId, locationId);

            if (price.PriceId == 0)
            {
                price.LocationId = locationId;
                price.PluCode = productId;
                price.RegisterDate = DateTime.Now;
                price.UserId = 111;
                price.Value = value;
            }
            else
            {
                price.RegisterDate = DateTime.Now;
                price.UserId = 111;
                price.Value = value;
            }

            return price;
        }

        private Location ValidateLocation(ProductCreateDto productCreateDto)
        {
            Location location = this.locationServices.GetByName(productCreateDto.Location);

            if (location.Name == null)
            {
                location.Name = productCreateDto.Location;
                this.locationServices.Create(location);
            }

            return location;
        }

        private Product ValidateProduct(ProductCreateDto productCreateDto)
        {
            Product product = this.productServices.GetById(productCreateDto.PluCode);

            if (product.Name == null)
            {
                product.Name = productCreateDto.Location;
                product.ProductId = productCreateDto.PluCode;
                this.productServices.Create(product);
            }

            return product;
        }

        private void Create(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Add(price);
        }
        private void Update(Price price)
        {
            if (price == null)
                throw new ArgumentNullException();

            this.priceRepository.Update(price);
        }
    }
}
