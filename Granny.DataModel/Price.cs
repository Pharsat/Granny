using System;
using System.Collections.Generic;

namespace Granny.DataModel
{
    public class Price
    {
        public int PriceId { get; set; }
        public int LocationId { get; set; }

        public string ProductId { get; set; }

        public int UserId { get; set; }
        public long PluCode { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }

        public IEnumerable<Product> OtherPrices { get; set; }

        public Product Product { get; set; }
        public Location Location { get; set; }
        public User User { get; set; }
    }
}
