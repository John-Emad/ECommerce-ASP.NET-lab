using Market.Data;
using Market.Models;
using Market.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class ProductController : Controller
    {
        //public MarketDbContext context = new MarketDbContext();

        IProductRepository productRepository;
        ICategoryRepository categoryRepository;

        public ProductController(IProductRepository _productRepository, ICategoryRepository _categoryRepository)
        {
            productRepository = _productRepository;
            categoryRepository = _categoryRepository;
        }

        // All Products View
        public IActionResult Index()
        {
            return View(productRepository.GetAll());
        }

        // Add Product View 
        public IActionResult AddProduct()
        {
            ViewBag.Categories = categoryRepository.GetAll();
            return View();
        }

        // Add Product Form
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
           productRepository.Add(product);
            var values = productRepository.Save();
            return RedirectToAction("index");
        }

        // Edit Product Form
        public IActionResult EditProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = productRepository.GetById(id.Value);
            if (product == null)
                return NotFound();
            ViewBag.Categories = categoryRepository.GetAll();
            return View(product);
        }

        // Edit Product
        [HttpPost]
        public IActionResult EditProduct(Product editedProduct)
        {
            productRepository.Update(editedProduct);
            productRepository.Save();
            return RedirectToAction("index");
        }

        // Product Details View
        public IActionResult ProductDetails(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = productRepository.GetById(id.Value);
            if (product == null)
                return NotFound();
            return View(product);
        }


        // Product Delete View
        public IActionResult Delete(int id)
        {
            Product product = productRepository.GetById(id);
            return View(product);
        }

        [HttpPost]
        [ActionName("DeleteProduct")]
        // Delete Product
        public IActionResult DeleteProduct(int? id)
        {
            if (id == null)
                return BadRequest();

            Product product = productRepository.GetById(id.Value);

            if (product == null)
                return NotFound();

            productRepository.Delete(product);
            productRepository.Save();
            return RedirectToAction("index");
        }

    }
}
