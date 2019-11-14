using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.Services.Interfaces;
using Granny.Util.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Query.Controllers.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BestPriceController : ControllerBase
    {

        private readonly IPriceServices _priceServices;
        private readonly IMapper _mapper;

        public BestPriceController(
            IPriceServices priceServices,
            IMapper mapper)
        {
            _priceServices = priceServices;
            _mapper = mapper;
        }

        // GET: api/BestPrice/GetByCode/5
        [HttpGet("GetByCode/{pluCode}", Name = "GetByCode")]
        public async Task<IActionResult> GetByCode(
            long pluCode)
        {
            Price price = await _priceServices.GetBestProductPrice(pluCode).ConfigureAwait(false);
            if (price == null) return Ok();

            var result = _mapper.Map<PriceOutputDto>(price);

            return Ok(result);
        }

        // GET: api/BestPrice/GetByLocation/Exito las vegas
        [HttpGet("GetByLocation/{location}", Name = "GetByLocation")]
        public async Task<IActionResult> GetByLocation(
            string location)
        {
            if (!ModelState.IsValid) return BadRequest();

            IEnumerable<Price> prices = await _priceServices.GetPricesByLocation(location).ConfigureAwait(false);

            IEnumerable<PriceOutputDto> result = from price in prices
                                                 select _mapper.Map<PriceOutputDto>(price);

            return Ok(result);
        }

        //GET: api/ProduBestPrice/5/2000
        [HttpGet("GetByCodeLowerPrices/{pluCode}/{value}", Name = "GetByCodeAfterPrice")]
        public async Task<IActionResult> GetByCodeAfterPrice(
                [FromQuery] long pluCode,
                [FromQuery, MinValue(typeof(decimal), "0")] decimal value)
        {
            if (!ModelState.IsValid) return BadRequest();

            IEnumerable<Price> prices = await _priceServices.GetNextProductPrices(pluCode, value).ConfigureAwait(false);

            IEnumerable<PriceOutputDto> result = from price in prices
                                                 select _mapper.Map<PriceOutputDto>(price);

            return Ok(result);
        }
    }
}
