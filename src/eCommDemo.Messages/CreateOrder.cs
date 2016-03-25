using System;
using System.Collections.Generic;

namespace eCommDemo.Messages
{
    public class CreateOrder
    {
        public Address ShippingAddress { get; set; }
        public Address BillingAddress { get; set; }
        public string AuthorizationCode { get; set; }
        public IList<Product> LineItems { get; set; }
    }

    public class Product
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public IList<ProductDetail> Details { get; set; }
    }

    public class ProductDetail
    {
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public IList<Tuple<int, int, int>> Colors { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string PostalCode { get; set; }
    }
}