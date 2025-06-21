using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Edit Category Form
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
                return BadRequest();

            Category category = context.Categories.FirstOrDefault(s => s.Id == id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        // Edit Category
        [HttpPost]
        public IActionResult EditCategory(Category editedCategory)
        {
            context.Update(editedCategory);
            context.SaveChanges();
            return RedirectToAction("index");
        }

        // Category Delete View
        public IActionResult Delete(int id)
        {
            Category category = context.Categories.FirstOrDefault(s => s.Id == id);
            return View(category);
        }

        [HttpPost]
        [ActionName("DeleteCategory")]
        // Delete Product
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
                return BadRequest();

            Category category = context.Categories.FirstOrDefault(s => s.Id == id);

            if (category == null)
                return NotFound();

            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
