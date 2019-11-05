using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.Services.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Register.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("GrannySafeOrigin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;
        private readonly ILocationServices _locationServices;
        private readonly IPriceServices _priceServices;

        public ProductController(ILocationServices locationServices, IProductServices productServices, IPriceServices priceServices)
        {
            _locationServices = locationServices;
            _productServices = productServices;
            _priceServices = priceServices;
        }

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PriceCreateDto newProductEntry)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _priceServices.Create(newProductEntry).ConfigureAwait(false);
            return Ok();
        }
    }
}
