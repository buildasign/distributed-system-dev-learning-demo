using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using eComm.Domain;
using Raven.Client;

namespace EcommApi.Controllers
{
    [RoutePrefix("catalog")]
    public class CatalogController : ApiController
    {
        private readonly IDocumentStore _documentStore;

        public CatalogController(IDocumentStore documentStore)
        {
            _documentStore = documentStore;
        }


        [Route("{categoryId}")]
        public IEnumerable<ListingData> Get(int categoryId)
        {
            List<ListingData> listingData;
            using (var session = _documentStore.OpenSession())
            {
                listingData = session.Query<ListingData>().ToList();
            }

            if (!listingData.Any())
            {
                AddItemsToCatalog();
                using (var session = _documentStore.OpenSession())
                {
                    listingData = session
                        .Query<ListingData>()
                        .Customize(x => x.WaitForNonStaleResults(TimeSpan.FromSeconds(5)))
                        .ToList();
                }
            }


            return listingData;
        }

        private void AddItemsToCatalog()
        {

            using (var session = _documentStore.OpenSession())
            {


                //TODO: return from Raven DB
                session.Store(
                    new ListingData
                    {
                        SKU = "CNV-123",
                        Height = 16,
                        Width = 20,
                        Name = "Arizona",
                        Image = "/Images/Listings/arizona.jpg",
                        Price = 100m
                    });
                session.Store(new ListingData
                {
                    SKU = "CNV-124",
                    Height = 20,
                    Width = 30,
                    Name = "Utah",
                    Image = "/Images/Listings/utah.jpg",
                    Price = 140m
                });
                session.Store(new ListingData
                {
                    SKU = "CNV-125",
                    Height = 16,
                    Width = 20,
                    Name = "Oregon",
                    Image = "/Images/Listings/oregon.jpg",
                    Price = 100m
                });

                session.Store(new ListingData
                {
                    SKU = "CNV-126",
                    Height = 30,
                    Width = 40,
                    Name = "New Mexico",
                    Image = "/Images/Listings/new_mexico.jpg",
                    Price = 160m
                });

                session.SaveChanges();
            }
        }
    }
}
