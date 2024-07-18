using BlogAppMVC.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogAppMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var user = new IdentityUser
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };
            var result = await userManager.CreateAsync(user, registerViewModel.Password);
            if (result.Succeeded)
            {
                var roleResult = await userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Register");
                }
            }

            return RedirectToAction("Register");
        }
        public IActionResult Login(string ReturnUrl)
        {
            var model = new LoginViewModel
            {
                ReturnUrl = ReturnUrl
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);
            if (result.Succeeded && result != null)
            {
                if(!string.IsNullOrEmpty(loginViewModel.ReturnUrl))
                {
                    return Redirect(loginViewModel.ReturnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
