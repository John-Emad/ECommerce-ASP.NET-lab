using Market.Models;
using Market.Repository;
using Market.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Market.Controllers
{
    public class AccountController : Controller
    {
        IUserRepository userRepository;
        IRoleRepository roleRepository;

        public AccountController(IUserRepository _userRepository, IRoleRepository _roleRepository)
        {
            userRepository = _userRepository;
            roleRepository = _roleRepository;
        }

        public IActionResult SignUp()
        {
            ViewBag.Roles = roleRepository.GetAll()
                .Select(r => new SelectListItem
                {
                    Value = r.Id.ToString(),
                    Text = r.Name
                }).ToList();

            return View(new User());
        }

        [HttpPost]
        public IActionResult SignUp(User model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = roleRepository.GetAll()
                    .Select(r => new SelectListItem
                    {
                        Value = r.Id.ToString(),
                        Text = r.Name
                    }).ToList();

                return View(model);
            }

            // Map selected role IDs to UserRoles
            model.UserRoles = model.SelectedRoleIds?.Select(roleId => new UserRole
            {
                RoleId = roleId
            }).ToList();

            userRepository.Add(model);
            userRepository.Save();


            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(UserLoginViewModel userLoginViewModel)
        {
            if (ModelState.IsValid)
            {
                User userFromDb = userRepository.GetByEmailAndPassword(userLoginViewModel.Email, userLoginViewModel.Password);

                if (userFromDb != null)
                {
                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userFromDb.FirstName),
                            new Claim(ClaimTypes.Email, userFromDb.Email)
                        };

                    // Add role claims (one per role)
                    foreach (var userRole in userFromDb.UserRoles)
                    {
                        string roleName = userRole.Role?.Name ?? "Unknown";
                        claims.Add(new Claim(ClaimTypes.Role, roleName));
                    }

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(principal); // Add claims to cookie

                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }
            return View(userLoginViewModel);
        }


        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
