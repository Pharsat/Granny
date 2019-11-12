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
    [Route("api/[controller]")]
    [ApiController]
    public class BestPriceController : ControllerBase
    {

        private readonly IPriceServices _priceServices;

        public BestPriceController(
            IPriceServices priceServices)
        {
            _priceServices = priceServices;
        }

        // GET: api/Product/5
        [HttpGet("{pluCode}", Name = "GetByCode")]
        public async Task<IActionResult> GetByCode(
            long pluCode)
        {
            Price price = await _priceServices.GetBestProductPrice(pluCode).ConfigureAwait(false);
            if (price == null) return NotFound();
            return Ok(price);
        }

        // GET: api/Product/Exito las vegas
        [HttpGet("{location}", Name = "GetByLocation")]
        public async Task<IActionResult> GetByLocation(
            string location)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetPricesByLocation(location).ConfigureAwait(false));
        }

        //GET: api/Product/5/2000
        [HttpGet("{pluCode}/{price}", Name = "GetByCodeAfterPrice")]
        public async Task<IActionResult> GetByCodeAfterPrice(
                long pluCode,
                [MinValue(typeof(decimal), "0")] decimal price)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetNextProductPrices(pluCode, price).ConfigureAwait(false));
        }
    }
}
