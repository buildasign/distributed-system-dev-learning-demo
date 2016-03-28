using System.Collections.Generic;
using System.Web.Http;

namespace EcommApi.Controllers
{
    public class CatalogController : ApiController
    {
        public IEnumerable<ListingData> Get(int categoryId)
        {
            //TODO: return from Raven DB
            return new[]
            {
                new ListingData
                {
                    SKU = "CNV-123",
                    Height= 16,
                    Width = 20,
                    Name = "Arizona",
                    Image = "/Images/Listings/arizona.jpg",
                    Price = 100m
                },
                new ListingData
                {
                    SKU = "CNV-124",
                    Height= 20,
                    Width = 30,
                    Name = "Utah",
                    Image = "/Images/Listings/utah.jpg",
                    Price = 140m
                },
                new ListingData
                {
                    SKU = "CNV-125",
                    Height= 16,
                    Width = 20,
                    Name = "Oregon",
                    Image = "/Images/Listings/oregon.jpg",
                    Price = 100m
                },
                new ListingData
                {
                    SKU = "CNV-126",
                    Height= 30,
                    Width = 40,
                    Name = "New Mexico",
                    Image = "/Images/Listings/new_mexico.jpg",
                    Price = 160m
                }
            };
        }
    }

    public class ListingData
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
    }
}
