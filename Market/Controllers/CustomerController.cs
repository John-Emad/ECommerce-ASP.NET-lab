using Market.Models;
using Market.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Market.Controllers
{
    public class CustomerController : Controller
    {
        //public MarketDbContext context = new MarketDbContext();
        ICustomerRepository customerRepository;
        public CustomerController(ICustomerRepository _customerRepository)
        {
            customerRepository = _customerRepository;
        }

        public ActionResult Index()
        {
            return View(customerRepository.GetAll());
        }

        public ActionResult CustomerDetails(int? id)
        {
            Customer customer = customerRepository.GetById(id.Value);
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
                customerRepository.Add(customer);
                var values = customerRepository.Save();
                return RedirectToAction("index");
            }
            ViewBag.Customer = customer;
            return View(customer);

        }
    }
}
