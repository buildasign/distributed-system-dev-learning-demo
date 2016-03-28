using AutoMapper;
using eComm.Domain;
using eCommDemo.Controllers;

namespace eCommDemo.Common
{
    public class MapperProfile:Profile
    {
        protected override void Configure()
        {
            CreateMap<ListingData, Category>();
        }
    }
}