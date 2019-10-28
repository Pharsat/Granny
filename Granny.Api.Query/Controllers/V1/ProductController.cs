using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetBestProductPrice(
            [Required, StringLength(14), RegularExpression("^[0-9]*$")] string pluCode)
        {
            return Ok(await repository.GetBestProductPrice(pluCode));
        }

        // GET: api/Product/5/2000
        [HttpGet("{pluCode}/{price}", Name = "GetNextProductPrices")]
        public async Task<IActionResult> GetNextProductPrices(
            [Required, StringLength(14), RegularExpression("^[0-9]*$")] string pluCode, 
            [Required, MinValue(typeof(decimal), "0")] decimal price)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await repository.GetNextProductPrices(pluCode, price));
        }

        // GET: api/Product/Exito las vegas
        [HttpGet("{location}", Name = "GetPricesByLocation")]
        public async Task<IActionResult> GetPricesByLocation(
            [Required] string location)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await repository.GetPricesByLocation(location));
        }
    }
}
