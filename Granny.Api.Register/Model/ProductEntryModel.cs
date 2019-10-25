using Granny.Api.Register.Validators;
using Granny.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Granny.Api.Register.Model
{
    public class ProductEntryModel
    {
        [Required]
        [StringLength(14)]
        public string PluCode { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [MinValue(typeof(decimal), "0")]
        public decimal Price { get; set; }
        [Required]
        public string Location { get; set; }
    }
}
