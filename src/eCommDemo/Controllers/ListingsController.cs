using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using eComm.Domain;
using eComm.Domain.Models;
using eCommDemo.Common;

namespace eCommDemo.Controllers
{
    public class ListingsController : BaseController
    {
        // GET: Listings
        public ActionResult Index(int? id)
        {
            var model = new ListingsModel();
            var query = new ListingQuery();
            model.Category = "Canvas";
            model.Listings = query.GetListingsByCategory(0);
            return View(model);
        }
    }

    public class ListingsModel
    {
        public bool Error { get; set; }
        public string Message { get; set; }
        public string Category { get; set; }
        public List<Listing> Listings { get; set; }
    }

    public class ListingQuery
    {
        public List<Listing> GetListingsByCategory(int categoryId)
        {
            var url = $"catalog/{categoryId}";
            var request = HttpUtil.CreateRequest(url, HttpMethod.Get);
            var response = HttpUtil.Send<IEnumerable<ListingData>>(request);

            return response.Select(l => new Listing {Name = l.Name, Price = l.Price, Image = l.Image, SKU = l.SKU, Size = $"{l.Height}x{l.Width}"}).ToList();

            ////TEMP CODE - replace with API call
            //return new List<Listing>
            //{
            //    new Listing
            //    {
            //        SKU = "CNV-123",
            //        Size="16x20",
            //        Name = "Arizona",
            //        Image = "/Images/Listings/arizona.jpg",
            //        Price = 100m
            //    },
            //    new Listing
            //    {
            //        SKU = "CNV-124",
            //        Size="20x30",
            //        Name = "Utah",
            //        Image = "/Images/Listings/utah.jpg",
            //        Price = 140m
            //    },
            //    new Listing
            //    {
            //        SKU = "CNV-125",
            //        Size="16x20",
            //        Name = "Oregon",
            //        Image = "/Images/Listings/oregon.jpg",
            //        Price = 100m
            //    },
            //    new Listing
            //    {
            //        SKU = "CNV-126",
            //        Size="30x40",
            //        Name = "New Mexico",
            //        Image = "/Images/Listings/new_mexico.jpg",
            //        Price = 160m
            //    }
            //};
        }
    }

    public class Listing
    {
        public string SKU { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Size { get; set; }
        public decimal Price { get; set; }
    }
}