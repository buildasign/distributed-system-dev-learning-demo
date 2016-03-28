using System;
using System.Linq;
using System.Web.Http;
using eComm.Domain;
using eCommApi.Common;
using eCommApi.Models;
using Raven.Client;

namespace eCommApi.Controllers
{
    [RoutePrefix("cart/")]
    public class CartController : ApiController
    {
        private readonly IDocumentStore _documentStore;

        public CartController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }


        // GET api/cart/5
        [Route("{tokenId}")]
        public Cart Get(Guid tokenId)
        {
            Cart cart;
            using (var session = _documentStore.OpenSession())
            {
                cart = session.Query<Cart>().FirstOrDefault(x => x.TokenId == tokenId);
            }

            return cart;
        }

        // POST api/cart
        [Route("")]
        public CartToken Post()
        {
            var cart = new Cart
            {
                TokenId = CombGuid.Generate()
            };

            using (var session = _documentStore.OpenSession())
            {
                session.Store(cart);

                session.SaveChanges();
            }

            return new CartToken(cart.TokenId, DateTime.Now);
        }

        [Route("{tokenId}/payment")]
        public PaymentAuth Post(Guid tokenId, CreatePayment createPayment)
        {
            PaymentAuth auth;
            using (var session = _documentStore.OpenSession())
            {
                var cart = session.Query<Cart>()
                    .FirstOrDefault(x => x.TokenId == tokenId);

                
                
                cart.AuthCode = "123456789";

                auth = new PaymentAuth
                {
                    AuthCode =  cart.AuthCode,
                    Authorized = true
                };

                session.SaveChanges();
            }

            return auth;
        }

    }
}