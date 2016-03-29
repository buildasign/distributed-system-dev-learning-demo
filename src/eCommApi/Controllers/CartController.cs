using System;
using System.Web.Http;
using System.Linq;
using System.Net;
using System.Net.Http;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Messages;
using EcommApi.Common;
using Raven.Client;

namespace EcommApi.Controllers
{
    [RoutePrefix("cart")]
    public class CartController : ApiController
    {
        private readonly IDocumentStore _documentStore;
        private readonly IEnterpriseBus _bus;
        private readonly ICreateOrderMapper _mapper;

        public CartController(IDocumentStore documentStore, IEnterpriseBus bus, ICreateOrderMapper mapper)
        {
            _documentStore = documentStore;
            _bus = bus;
            _mapper = mapper;
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

        [Route("{tokenId}/checkout")]
        public HttpResponseMessage Post(Guid tokenId, CustomerInfo customerInfo)
        {
            int cartId=0;
            using (var session = _documentStore.OpenSession())
            {
                var cart = session.Query<Cart>()
                    .FirstOrDefault(x => x.TokenId == tokenId);
                if (string.IsNullOrEmpty(cart?.AuthCode))
                {
                    throw new Exception();
                }
                cartId = cart.Id;
                var message = _mapper.Map(customerInfo, cart);
                _bus.Publish(message);

            }
            var response = Request.CreateResponse(HttpStatusCode.OK, cartId);
            return response;
        }
    }
}