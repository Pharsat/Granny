using System.Collections.Generic;

namespace Granny.DataTransferObject.Price
{
    public class PriceDto
    {
        public string PluCode { get; set; }
        public string ProductName { get; set; }
        public string Location { get; set; }
        public decimal Value { get; set; }
        public IEnumerable<PriceDto> OtherPrices { get; set; }
    }
}
