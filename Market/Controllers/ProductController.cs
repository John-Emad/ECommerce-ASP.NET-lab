using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        public MarketDbContext context = new MarketDbContext();

        // View Products
        public IActionResult Index()
        {
            return View(context.Products.Include(p => p.Category).ToList());
        }

        // Add Product View 
        public IActionResult AddProduct()
        {
            ViewBag.Categories = context.Categories.ToList();
            return View();
        }

        // Add Product Form
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            context.Products.Add(product);
            var values = context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
