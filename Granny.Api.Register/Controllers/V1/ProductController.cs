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
        public async Task<IActionResult> Post(
            [FromBody, Required] ProductCreateDto newProductEntry)
        {
            if (ModelState.IsValid)
            {
                Product product = new Product() { 
                    Name = newProductEntry.Name,
                    ProductId = newProductEntry.PluCode
                };

                this.priceServices.Register(newProductEntry);
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

    }
}
