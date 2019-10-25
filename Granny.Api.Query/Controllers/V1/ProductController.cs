using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Granny.Api.Query.Model;
using Granny.Repository.Query;
using Granny.Util.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Query.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IQueryRepository repository;

        // GET: api/Product/5
        [HttpGet("{pluCode}", Name = "GetBestProductPrice")]
        public async Task<IActionResult> GetBestProductPrice([Required][StringLength(14)][RegularExpression("^[0-9]*$")] string pluCode)
        {
            return Ok(await repository.GetBestProductPrice(pluCode));
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> GetNextProductPrices([FromBody] GetNextPricesRequestModel value)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await repository.GetNextProductPrices(value.PluCode, value.Price));
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> GetPricesByLocation([FromBody] GetPricesByLocationRequestModel value)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await repository.GetPricesByLocation(value.Location));
        }
    }
}
