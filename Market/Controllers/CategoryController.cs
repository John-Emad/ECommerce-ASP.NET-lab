using Market.Data;
using Market.Models;
using Market.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class CategoryController : Controller
    {
        //public MarketDbContext context = new MarketDbContext();

        ICategoryRepository categoryRepository;

        public CategoryController(ICategoryRepository _categoryRepository)
        {
            categoryRepository = _categoryRepository;
        }

        // View Categories
        public IActionResult Index()
        {
            return View(categoryRepository.GetAll());
        }
        
        // Add Category View
        public IActionResult AddCategory()
        {
            ViewBag.Categories = categoryRepository.GetAll();
            return View();
        }

        // Add Category Form
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            categoryRepository.Add(category);
            var values = categoryRepository.Save();
            return RedirectToAction("index");
        }

        // Edit Category Form
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
                return BadRequest();

            Category category = categoryRepository.GetById(id.Value);
            if (category == null)
                return NotFound();
            return View(category);
        }

        // Edit Category
        [HttpPost]
        public IActionResult EditCategory(Category editedCategory)
        {
            categoryRepository.Update(editedCategory);
            categoryRepository.Save();
            return RedirectToAction("index");
        }

        // Category Delete View
        public IActionResult Delete(int id)
        {
            Category category = categoryRepository.GetById(id);
            return View(category);
        }

        [HttpPost]
        [ActionName("DeleteCategory")]
        // Delete Product
        public IActionResult DeleteCategory(int? id)
        {
            if (id == null)
                return BadRequest();

            Category category = categoryRepository.GetById(id.Value);

            if (category == null)
                return NotFound();

            categoryRepository.Delete(category);
            categoryRepository.Save();
            return RedirectToAction("index");
        }
    }
}
