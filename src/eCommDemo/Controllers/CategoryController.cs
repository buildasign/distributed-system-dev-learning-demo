using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eCommDemo.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index()
        {
            var model = new CategoriesModel();
            return View(model);
        }
    }

    public class CategoriesModel
    {
        public CategoriesModel()
        {
            Categories = new List<Category>(new[] { new Category { Id = 1, Name = "Single Canvas", Image = "/Images/Categories/Canvas.png" }, new Category { Id = 2, Name = "Wall Display", Image = "/Images/Categories/WallDisplay.png" }});
        }

        public List<Category> Categories { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}