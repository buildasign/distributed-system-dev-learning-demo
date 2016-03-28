using AutoMapper;
using eComm.Domain;
using eCommDemo.Controllers;
using eCommDemo.Models;

namespace eCommDemo.Common
{
    public class MapperProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<ListingData, Category>();
            CreateMap<CartItem, CartItemModel>();
        }
    }
}