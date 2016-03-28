using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public class ListingsController : Controller
    {
        // GET: Listings
        public ActionResult Index(int id)
        {
            var sessionId = this.Request.Cookies["usersession"];
            if (string.IsNullOrEmpty(sessionId))
            {
                this.Response.Cookies.Add(new HttpCookie("usersession", this.Session.SessionID));
            }

            var model = new ListingsModel();
            var query = new ListingQuery();
            model.Category = "Canvas";
            model.Listings = query.GetListingsByCategory(id);
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
            //TEMP CODE - replace with API call
            return new List<Listing>
            {
                new Listing
                {
                    SKU = "CNV-123",
                    Size="16x20",
                    Name = "Arizona",
                    Image = "/Images/Listings/arizona.jpg",
                    Price = 100m
                },
                new Listing
                {
                    SKU = "CNV-124",
                    Size="20x30",
                    Name = "Utah",
                    Image = "/Images/Listings/utah.jpg",
                    Price = 140m
                },
                new Listing
                {
                    SKU = "CNV-125",
                    Size="16x20",
                    Name = "Oregon",
                    Image = "/Images/Listings/oregon.jpg",
                    Price = 100m
                },
                new Listing
                {
                    SKU = "CNV-126",
                    Size="30x40",
                    Name = "New Mexico",
                    Image = "/Images/Listings/new_mexico.jpg",
                    Price = 160m
                }
            };
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