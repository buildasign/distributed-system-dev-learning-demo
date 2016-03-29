using System.Collections.Generic;
using System.Linq;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Messages;

namespace EcommApi.Controllers
{
    public interface ICreateOrderMapper
    {
        CreateOrder Map(CustomerInfo model, Cart cart);
    }
    public class CreateOrderMapper : ICreateOrderMapper
    {
        public CreateOrder Map(CustomerInfo model, Cart cart)
        {
            return new CreateOrder
            {
                AuthorizationCode = cart.AuthCode,
                BillingAddress = new Address { City = model.City, Address1 = model.Address, PostalCode = model.Zip, StateRegion = model.State },
                ShippingAddress = new Address { City = model.City, Address1 = model.Address, PostalCode = model.Zip, StateRegion = model.State },
                LineItems = MapCart(cart)
            };
        }

        private IList<Product> MapCart(Cart cart)
        {
            return cart.Items.Select(ci => new Product { SKU = ci.SKU, Quantity = ci.Quantity }).ToList();
        }
    }
}