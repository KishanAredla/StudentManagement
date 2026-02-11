using Microsoft.AspNetCore.Mvc;
using Student_WebApp.Models;
using Student_WebApp.Services;

namespace Student_WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AuthService _auth;

        public AccountController(AuthService auth)
        {
            _auth = auth;
        }

        // ✅ REQUIRED — shows login page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // ✅ handles form submit
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var token = await _auth.LoginAsync(model);

            HttpContext.Session.SetString("JWT", token);

            return RedirectToAction("Index", "Student");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();   // removes JWT
            return RedirectToAction("Login");
        }
    }

}
