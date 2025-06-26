using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Market.Models;
using Market.Repository;

namespace Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public IActionResult Index()
        {
            var roles = _roleRepository.GetAll();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Role role)
        {
            if (ModelState.IsValid)
            {
                _roleRepository.Add(role);
                _roleRepository.Save();
                return RedirectToAction("index");
            }

            return View(role);
        }

        public IActionResult Delete(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null) return NotFound();
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var role = _roleRepository.GetById(id);
            if (role == null) return NotFound();

            _roleRepository.Remove(role);
            _roleRepository.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
