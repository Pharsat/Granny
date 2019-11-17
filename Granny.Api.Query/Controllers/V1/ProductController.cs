using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Granny.DataModel;
using Granny.DataTransferObject.Price;
using Granny.Services.Interfaces;
using Granny.Util.Exceptions;
using Granny.Util.Search_GS1;
using Granny.Util.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Granny.Api.Query.Controllers.V1
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPriceServices _priceServices;
        private readonly IMapper _mapper;

        public ProductController(IPriceServices priceServices, IMapper mapper)
        {
            _priceServices = priceServices;
            _mapper = mapper;
        }

        [HttpGet("Get/{nameProduct}", Name = "Get")]
        public async Task<IActionResult> Get(
            string nameProduct)
        {
            if (!ModelState.IsValid) return BadRequest();

            IEnumerable<Price> prices = await _priceServices.GetByName(nameProduct).ConfigureAwait(false);

            IEnumerable<PriceOutputDto> result = from price in prices
                                                 select _mapper.Map<PriceOutputDto>(price);

            return Ok(result);
        }

        [HttpGet("Product/{productId}/GetByName", Name = "GetByName")]
        public async Task<IActionResult> GetByName(
           long productId)
        {
            try
            {
                return Ok(await SearchProductName.SearchProductAsync(productId.ToString()).ConfigureAwait(false));
            }
            catch (ExternalRequestException)
            {
                return Ok();
            }
            catch (ProductNotFoundException)
            {
                return Ok();
            }
        }
    }
}