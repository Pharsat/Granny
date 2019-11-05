using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Granny.Api.Register.Model;
using Granny.DAO.EntitiesRepository;
using Granny.DAO.EntitiesRepository.Interface;
using Granny.DAO.UnitOfWork;
using Granny.DAO.UnitOfWork.Interface;
using Granny.DataModel;
using Granny.Repository.Register;
using Granny.Services;
using Granny.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Register.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("GrannySafeOrigin")]
    public class ProductController : ControllerBase
    {
        //private readonly IRegisterRepository repository;
        private readonly IProductRepository productRepository;
        private readonly IProductServices productServices;
        //private readonly ILocationRepository locationRepository;
        private readonly ILocationServices locationServices;
        //private readonly IPriceRepository priceRepository;
        private readonly IPriceServices priceServices;
        private readonly IUnitOfWork unitOfWork;

        public ProductController()
        {
            this.unitOfWork = new UnitOfWork();
            this.productRepository = new ProductRepository(this.unitOfWork);
            this.productServices = new ProductServices(
                this.unitOfWork, 
                this.productRepository,
                this.locationServices,
                this.priceServices);
            //this.locationRepository = new LocationRepository(this.unitOfWork);
            //this.locationServices = new LocationServices(this.unitOfWork, this.locationRepository);
            //this.priceRepository = new PriceRepository(this.unitOfWork);
            //this.priceServices = new PriceServices(this.unitOfWork, this.priceRepository);
        }


        // POST: api/Product
        [HttpPost]
        //public Task<IActionResult> Post(
        public IActionResult Post(
            [FromBody, Required] ProductCreateDto productCreateDto)
        {
            if (ModelState.IsValid)
            {
                //Product product = new Product() { 
                //    Name = productCreateDto.Name,
                //    ProductId = productCreateDto.PluCode
                //};

                //this.priceServices.Register(productCreateDto);

                Location location = this.ValidateLocation(productCreateDto);
                Product product = this.ValidateProduct(productCreateDto);
                Price price = this.ValidatePrice(product.ProductId, location.LocationId, productCreateDto.Price);
                               
                return Ok();
            }
            else
            {
                return BadRequest();
            }

            //if (newProductEntry != null && !ModelState.IsValid) return BadRequest();
            ////TODO: Validation of existing product should be made on DB.
            //await repository.InsertProduct(new Product
            //{
            //    Name = newProductEntry.Name,
            //    PluCode = newProductEntry.PluCode
            //}).ConfigureAwait(false);
            ////TODO: Validation about existing price should be made on DB.
            //int priceId = await repository.InsertPrice(new Price
            //{
            //    LocationId = 0, //TODO: ask for locations 
            //    PluCode = newProductEntry.PluCode,
            //    RegisterDate = DateTime.Now,
            //    Value = newProductEntry.Price,
            //    UserId = 0
            //}).ConfigureAwait(false);
            //return Ok(priceId);
        }

        private Price ValidatePrice(long productId, int locationId, decimal value)
        {
            Price price = this.priceServices.GetByProductLocation(productId, locationId);

            if (price.PriceId == 0)
            {
                price.LocationId = locationId;
                price.PluCode = productId;
                price.RegisterDate = DateTime.Now;
                price.UserId = 111;
                price.Value = value;
                this.priceServices.Create(price);
            }
            else
            {
                price.RegisterDate = DateTime.Now;
                price.UserId = 111;
                price.Value = value;
                this.priceServices.Update(price);
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

    }
}
