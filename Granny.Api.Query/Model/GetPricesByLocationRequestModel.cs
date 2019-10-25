using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.Api.Query.Model
{
    public class GetPricesByLocationRequestModel
    {
        [Required]
        public string Location { get; set; }
    }
}
