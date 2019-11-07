using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Granny.Api.Register.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    //[EnableCors("GrannySafeOrigin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ILocationServices _locationServices;
        private readonly IPriceServices _priceServices;
        private IMapper _mapper;

        public ProductController(ILocationServices locationServices, IProductServices productServices, IPriceServices priceServices, IMapper mapper)
        {
            _locationServices = locationServices;
            _productServices = productServices;
            _priceServices = priceServices;
            _mapper = mapper;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PriceCreateDto priceDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            //await _priceServices.Create(newProductEntry).ConfigureAwait(false);

            Location location = await this._locationServices.GetByName(priceDto.Location).ConfigureAwait(false);

            if (location == null)
            {
                //location = _mapper.Map<Location>(priceDto);
                location = new Location();
                location.Name = priceDto.Location;
                location.LocationId = await this._locationServices.Create(location).ConfigureAwait(false);
            }

            Product product = this._productServices.GetById(priceDto.PluCode);

            if (product == null)
            {
                product = new Product();
                product.Name = priceDto.Name;
                product.ProductId = priceDto.PluCode;
                await _productServices.Create(product).ConfigureAwait(false);
            }

            if (await _priceServices.CheckIfExists(product.ProductId, location.LocationId).ConfigureAwait(false) == null)
            {
                //TODO: _mapper.Map<Price>(priceDto).Map(product).Map(location);
                Price price = new Price();
                price.LocationId = location.LocationId;
                price.ProductId = product.ProductId;
                price.RegisterDate = DateTime.Now;
                price.UserId = 1;
                price.Value = priceDto.Price;

                await _priceServices.Create(price).ConfigureAwait(false);
            }

            return Ok();
        }

        private void Register(PriceCreateDto newProductEntry)
        {
            
        }
    }
}
