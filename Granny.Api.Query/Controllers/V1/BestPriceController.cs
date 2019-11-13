using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Granny.DataModel;
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

        public BestPriceController(
            IPriceServices priceServices)
        {
            _priceServices = priceServices;
        }

        // GET: api/BestPrice/GetByCode/5
        [HttpGet("GetByCode/{pluCode}", Name = "GetByCode")]
        public async Task<IActionResult> GetByCode(
            long pluCode)
        {
            Price price = await _priceServices.GetBestProductPrice(pluCode).ConfigureAwait(false);
            if (price == null) return Ok();
            return Ok(price);
        }

        // GET: api/BestPrice/GetByLocation/Exito las vegas
        [HttpGet("GetByLocation/{location}", Name = "GetByLocation")]
        public async Task<IActionResult> GetByLocation(
            string location)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetPricesByLocation(location).ConfigureAwait(false));
        }

        //GET: api/ProduBestPrice/5/2000
        [HttpGet("GetByCodeLowerPrices/{pluCode}/{price}", Name = "GetByCodeAfterPrice")]
        public async Task<IActionResult> GetByCodeAfterPrice(
                [FromQuery] long pluCode,
                [FromQuery, MinValue(typeof(decimal), "0")] decimal price)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetNextProductPrices(pluCode, price).ConfigureAwait(false));
        }
    }
}
