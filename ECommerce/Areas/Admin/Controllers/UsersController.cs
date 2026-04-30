using ECommerce.DataAccess.Identity;
using ECommerce.Presentation.Modules.Account.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperAdmin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreateAdmin(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(RegisterVM registerVM, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(registerVM);
            }
            var user = new ApplicationUser
            {
                 FullName = registerVM.FullName,
                 Email = registerVM.Email,
                 UserName = registerVM.Email
            };
            var result = await _userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded) 
            {
                foreach (var eror in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty, eror.Description);
                }
                return View(registerVM);
            }
            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
            if (!roleResult.Succeeded)
            {
                foreach (var eror in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, eror.Description);
                }
                return View(registerVM);
            }
            var claimResult = await _userManager.AddClaimAsync(user, new Claim  ("FullName", user.FullName));
            if (!claimResult.Succeeded)
            {
                foreach (var eror in claimResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, eror.Description);
                }
                return View(registerVM);
            }
            return RedirectToAction(nameof(AdminCreated), new {fullname =user.FullName});
        }
        public IActionResult AdminCreated(string fullname)
        {
            ViewData["AdminName"] = fullname;
            return View();
        }
    }
}
