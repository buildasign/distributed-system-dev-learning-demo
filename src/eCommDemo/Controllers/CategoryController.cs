using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using eComm.Domain;
using eCommDemo.Common;

namespace eCommDemo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper)
        {
            _mapper = mapper;
        }

        // GET: Category
        public ActionResult Index()
        {

            var request = HttpUtil.CreateRequest("catalog/0", HttpMethod.Get);

            var modelData = _mapper.Map<IEnumerable<ListingData>, IEnumerable<Category>>(HttpUtil.Send<IEnumerable<ListingData>>(request));

            return View(new CategoriesModel(modelData));
        }
    }

    public class CategoriesModel
    {
        public CategoriesModel(IEnumerable<Category> category)
        {
            Categories = category;
        }

        public IEnumerable<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}