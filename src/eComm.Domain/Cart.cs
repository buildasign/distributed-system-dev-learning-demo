using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid TokenId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
