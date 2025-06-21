using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        public MarketDbContext context = new MarketDbContext();

        // All Products View
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

        // Edit Product Form
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = context.Products.FirstOrDefault(s => s.Id == id);
            if (product == null)
                return NotFound();
            ViewBag.Categories = context.Categories.ToList();
            return View(product);
        }

        // Edit Product
        [HttpPost]
        public IActionResult EditProduct(Product editedProduct)
        {
            context.Update(editedProduct);
            context.SaveChanges();
            return RedirectToAction("index");
        }

        // Product Details View
        public IActionResult ProductDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = context.Products.Include(p=>p.Category).FirstOrDefault(s => s.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }


        // Product Delete View
        public IActionResult Delete(int id)
        {
            Product product = context.Products.FirstOrDefault(s => s.Id == id);
            return View(product);
        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        // Delete Product
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = context.Products.FirstOrDefault(s => s.Id == id);

            if (product == null)
                return NotFound();

            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("index");
        }

    }
}
