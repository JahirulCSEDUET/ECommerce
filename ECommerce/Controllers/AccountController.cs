using ECommerce.DataAccess.Identity;
using ECommerce.Presentation.Modules.Account.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualBasic;
using System.Security.Claims;

namespace ECommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVm, string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }
            var user = new ApplicationUser
            {
                UserName= registerVm.Email,
                FullName = registerVm.FullName,
                Email = registerVm.Email
            };
            var result =await _userManager.CreateAsync(user, registerVm.Password);
            if (!result.Succeeded) 
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVm);
            }
            //Assign to default role to every newly register User: Customer
            var roleResult = await _userManager.AddToRoleAsync(user, "Customer");
            if (!roleResult.Succeeded) 
            {
                foreach (var error in roleResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVm);
            }
            var claimResult = await _userManager.AddClaimAsync(user, new Claim ("FullName", user.FullName));
            if (!claimResult.Succeeded)
            {
                foreach (var error in claimResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVm);
            }
            await _signInManager.SignInAsync(user, isPersistent: true);
            return RedirectToReturnUrl(returnUrl);
        }
        public IActionResult Login(string? returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string? returnUrl) 
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded) 
            {
                return RedirectToReturnUrl(returnUrl);
            }
            ModelState.AddModelError(string.Empty, "Ivalid username or password");
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private IActionResult RedirectToReturnUrl(string? returnUrl)
        {
            if (!string.IsNullOrEmpty(returnUrl) && returnUrl != null)
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        
    }
}
