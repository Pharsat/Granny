using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Granny.Api.Register.Model;
using Granny.DataModel;
using Granny.Repository.Register;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Register.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    [EnableCors("GrannySafeOrigin")]
    public class ProductController : ControllerBase
    {

        private readonly IRegisterRepository repository;

        // POST: api/Product
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody, Required] ProductCreateDto newProductEntry)
        {
            if (newProductEntry is null)
            {
                throw new ArgumentNullException(nameof(newProductEntry));
            }

            if (newProductEntry != null && !ModelState.IsValid) return BadRequest();
            //TODO: Validation of existing product should be made on DB.
            await repository.InsertProduct(new Product
            {
                Name = newProductEntry.Name,
                PluCode = newProductEntry.PluCode
            }).ConfigureAwait(false);
            //TODO: Validation about existing price should be made on DB.
            int priceId = await repository.InsertPrice(new Price
            {
                LocationId = 0, //TODO: ask for locations 
                PluCode = newProductEntry.PluCode,
                RegisterDate = DateTime.Now,
                Value = newProductEntry.Price,
                UserId = 0
            }).ConfigureAwait(false);
            return Ok(priceId);
        }
    }
}
