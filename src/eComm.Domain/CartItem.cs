using System;

namespace eComm.Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid TokenId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }        
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
    }
}