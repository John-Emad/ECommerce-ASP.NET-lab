using Market.Data;
using Market.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Market.Controllers
{
    public class CustomerController : Controller
    {
        public MarketDbContext context = new MarketDbContext();

        public ActionResult Index()
        {
            return View(context.Customers.ToList());
        }

        public ActionResult CustomerDetails(int? id)
        {
            Customer customer = context.Customers.FirstOrDefault(s => s.Id == id);
            if (customer == null)
                return NotFound();
            return View(customer);
        }

        public IActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                context.Customers.Add(customer);
                var values = context.SaveChanges();
                return RedirectToAction("index");
            }
            ViewBag.Customer = customer;
            return View(customer);

        }
    }
}
