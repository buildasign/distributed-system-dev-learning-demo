using System;

namespace eCommDemo.Models
{
    public class CartItemModel
    {
        public Guid Id { get; set; }
        public Guid TokenId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public string Image { get; set; }
    }
}