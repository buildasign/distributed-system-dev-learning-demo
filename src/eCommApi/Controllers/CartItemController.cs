using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using eComm.Domain;
using eComm.Domain.Models;
using EcommApi.Common;
using Raven.Client;

namespace EcommApi.Controllers
{
    [RoutePrefix("cart/{tokenId}/item")]
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
                    SKU = createCartItem.SKU,
                    Quantity = createCartItem.Quantity,
                    TokenId = tokenId
                };

                if(cart.Items == null) cart.Items = new List<CartItem>();

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

        [Route("{id}/")]
        public string Delete(Guid tokenId, Guid id)
        {
            using (var session = _documentStore.OpenSession())
            {
                var cart = session.Query<Cart>().FirstOrDefault(x => x.TokenId == tokenId);

                if (cart != null)
                {
                    var item = cart.Items.FirstOrDefault(x => x.Id == id);

                    cart.Items.Remove(item);


                    session.SaveChanges();

                }
            }

            return id.ToString();
        }
    }
}