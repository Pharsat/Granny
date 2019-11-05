using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading.Tasks;
using Granny.Services.Interfaces;
using Granny.Util.Validators;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Query.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("GrannySafeOrigin")]
    public class ProductController : ControllerBase
    {
        private readonly IPriceServices _priceServices;

        public ProductController(IPriceServices priceServices)
        {
            _priceServices = priceServices;
        }

        // GET: api/Product/5
        [HttpGet("{pluCode}", Name = "GetBestProductPrice")]
        public async Task<IActionResult> GetBestProductPrice(
            [Required, StringLength(14), RegularExpression("^[0-9]*$")] string pluCode)
        {
            return Ok(await _priceServices.GetBestProductPrice(pluCode).ConfigureAwait(false));
        }

        // GET: api/Product/5/2000
        [HttpGet("{pluCode}/{price}", Name = "GetNextProductPrices")]
        public async Task<IActionResult> GetNextProductPrices(
            [Required, StringLength(14), RegularExpression("^[0-9]*$")] string pluCode, 
            [Required, MinValue(typeof(decimal), "0")] decimal price)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetNextProductPrices(pluCode, price).ConfigureAwait(false));
        }

        // GET: api/Product/Exito las vegas
        [HttpGet("{location}", Name = "GetPricesByLocation")]
        public async Task<IActionResult> GetPricesByLocation(
            [Required] string location)
        {
            if (!ModelState.IsValid) return BadRequest();
            return Ok(await _priceServices.GetPricesByLocation(location).ConfigureAwait(false));
        }
    }
}
