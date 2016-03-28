using System;
using System.Collections.Generic;
using System.Web.Http;

namespace eCommApi.Controllers
{
    public class CartController : ApiController
    {
        // GET api/cart/<guid>
        public IEnumerable<CartData> Get(Guid sessionId)
        {
            //TODO: get from Raven
            return new CartData[] {new CartData { Quantity = 1, SKU = "CNV-123" }, new CartData { Quantity = 1, SKU = "CNV-124" }, };
        }

        // GET api/cart/5
        public CartData Get(int id)
        {
            //TODO: get from Raven
            return new CartData {Quantity = 1, SKU = "CNV-123"};
        }

        // POST api/cart
        public int Post([FromBody]CartData data)
        {
            //TODO: add to Raven
            return 1;   //TODO: return cart id
        }

        // PUT api/cart/5
        public void Put(int id, [FromBody]CartData data)
        {
            //TODO: update in Raven
        }

        // DELETE api/cart/5
        public void Delete(int id, [FromBody]CartData data)
        {
            //TODO: remove from Raven
        }

    }

    public class CartData
    {
        public Guid SessionId { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int CartItemId { get; set; }
    }
}
