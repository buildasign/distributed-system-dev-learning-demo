using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using eComm.Domain;
using eCommApi.Common;
using eCommApi.Models;
using Raven.Client;

namespace eCommApi.Controllers
{
    [RoutePrefix("cart/{tokenId}/")]
    public class CartItemController : ApiController
    {
        private readonly IDocumentStore _documentStore;

        public CartItemController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }

        [Route("{id}")]
        public CartItem Get(Guid tokenId, Guid id)
        {
            CartItem cartItem;
            using (var session = _documentStore.OpenSession())
            {
                cartItem = session.Query<Cart>()
                    .FirstOrDefault(x => x.TokenId == tokenId)
                    .Items.FirstOrDefault(x => x.Id == id);
            }

            return cartItem;
        }

        [Route("")]
        public IEnumerable<CartItem> Get(Guid tokenId)
        {
            IEnumerable<CartItem> cartItems;
            using (var session = _documentStore.OpenSession())
            {
                cartItems = session.Query<Cart>()
                    .FirstOrDefault(x => x.TokenId == tokenId).Items;
            }

            return cartItems;
        }

        [Route("")]
        public string Post(Guid tokenId, CreateCartItem createCartItem)
        {
            CartItem cartItem;
            using (var session = _documentStore.OpenSession())
            {
                var cart = session.Query<Cart>()
                    .FirstOrDefault(x => x.TokenId == tokenId);

                cartItem = new CartItem
                {
                    Id = CombGuid.Generate(),
                    ProductId = createCartItem.ProductId,
                    Quantity = createCartItem.Quantity,
                    TokenId = tokenId
                };

                cart.Items.Add(cartItem);


                session.SaveChanges();
            }

            return cartItem.Id.ToString();
        }

        [Route("{id}/")]
        public string Put(Guid tokenId, Guid id, UpdateCartItem updateCartItem)
        {
            using (var session = _documentStore.OpenSession())
            {
                var cart = session.Query<Cart>().FirstOrDefault(x => x.TokenId == tokenId);

                if (cart != null)
                {
                    var item = cart.Items.FirstOrDefault(x => x.Id == id);

                    cart.Items.Remove(item);

                    item.Quantity = updateCartItem.Quantity;

                    cart.Items.Add(item);

                    session.SaveChanges();

                }
            }

            return id.ToString();
        }

    }
}