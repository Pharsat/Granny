using System;
using System.Collections.Generic;

namespace Granny.DataModel
{
    public class Price
    {
        public int PriceId { get; set; }
        public int LocationId { get; set; }
        public long ProductId { get; set; }
        public int UserId { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
