using Granny.Util.Validators;
using System.ComponentModel.DataAnnotations;

namespace Granny.DataTransferObject.Price
{
    public class PriceCreateDto
    {
        [Required]
        public long PluCode { get; set; }

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
