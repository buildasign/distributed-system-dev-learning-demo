using System;
using System.Collections.Generic;

namespace eComm.Domain
{
    public class Cart
    {
        public int Id { get; set; }
        public Guid TokenId { get; set; }
        public DateTime Created { get; set; }
        public IList<CartItem> Items { get; set; }
        public string PromotionCode { get; set; }
        public string AuthCode { get; set; }
    }
}