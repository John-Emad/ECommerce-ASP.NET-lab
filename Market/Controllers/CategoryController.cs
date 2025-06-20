using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class CategoryController : Controller
    {
        public MarketDbContext context = new MarketDbContext();
        
        // View Categories
        public IActionResult Index()
        {
            return View(context.Categories.ToList());
        }
        
        // Add Category View
        public IActionResult AddCategory()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }

        // Add Category Form
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            context.Categories.Add(category);
            var values = context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
