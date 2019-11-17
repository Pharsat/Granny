using System;

namespace Granny.DataTransferObject.Price
{
    public class PriceOutputDto
    {
        public int PriceId { get; set; }
        public int LocationId { get; set; }
        public long ProductId { get; set; }
        public string UserId { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
        public string ProductName { get;set; }
        public string LocationName { get; set; }
    }
}
