using CleanArchMvc.Domain.Account;
using CleanArchMvc.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthenticate _authenticate;
        public AccountController(IAuthenticate authenticate)
        {
            _authenticate = authenticate;
        }
        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var result = await _authenticate.AuthenticateAsync(loginViewModel.Email, loginViewModel.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong");
                return View(loginViewModel);
            }
            if (string.IsNullOrEmpty(loginViewModel.ReturnUrl))
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Index", loginViewModel.ReturnUrl);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var result = await _authenticate.RegisterUserAsync(registerViewModel.Email, registerViewModel.Password);
            if (!result)
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.(password must be strong");
                return View(registerViewModel);
            }
            return Redirect("/");
        }


        public async Task<IActionResult> Logout()
        {
            await _authenticate.Logout();
            return Redirect("/Account/Login");
        }
    }
}
