using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Granny.Api.Register.Controllers.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ILocationServices _locationServices;
        private readonly IPriceServices _priceServices;
        private IMapper _mapper;

        public PriceController(ILocationServices locationServices, IProductServices productServices, IPriceServices priceServices, IMapper mapper)
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

            Location location = await _locationServices.GetByName(priceDto.Location).ConfigureAwait(false);

            if (location == null)
            {
                location = _mapper.Map<Location>(priceDto);
                location.LocationId = await this._locationServices.Create(location).ConfigureAwait(false);
            }

            Product product = this._productServices.GetById(priceDto.PluCode);

            if (product == null)
            {
                product = _mapper.Map<Product>(priceDto);
                await _productServices.Create(product).ConfigureAwait(false);
            }

            Price price = null;

            if (await _priceServices.CheckIfExists(product.ProductId, location.LocationId).ConfigureAwait(false) == null)
            {
                price = _mapper.Map<Price>(priceDto);
                price.LocationId = location.LocationId;
                price.ProductId = product.ProductId;
                price.UserId = User.Identity.Name;
                price.PriceId = await _priceServices.Create(price).ConfigureAwait(false);
            }

            if (price != null) return Ok(price);

            return Conflict(new { message = "Price already exists" });
        }

    }
}
