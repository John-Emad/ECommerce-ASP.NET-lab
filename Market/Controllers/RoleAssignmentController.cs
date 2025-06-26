using Market.Models;
using Market.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Market.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleAssignmentController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public RoleAssignmentController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }

        // List all users with roles
        public IActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        // GET: Assign roles to a user
        public IActionResult Assign(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound();

            ViewBag.Roles = _roleRepository.GetAll()
                .Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Name })
                .ToList();

            var model = new User
            {
                Id = user.Id,
                FirstName = user.FirstName,
                SelectedRoleIds = user.UserRoles.Select(ur => ur.RoleId).ToList()
            };

            return View(model);
        }

        // POST: Save role assignments
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(User user)
        {
            var userFromDb = _userRepository.GetById(user.Id);
            if (userFromDb == null) return NotFound();

            // Clear current roles
            userFromDb.UserRoles.Clear();

            // Assign new roles
            if (user.SelectedRoleIds != null)
            {
                foreach (var roleId in user.SelectedRoleIds)
                {
                    userFromDb.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = roleId });
                }
            }

            _userRepository.Update(userFromDb);
            _userRepository.Save();

            return RedirectToAction("Index", "User"); // Adjust this to your user listing page
        }
    }
}
