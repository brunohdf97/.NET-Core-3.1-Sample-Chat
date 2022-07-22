using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Chat.Extensions;
using Chat.Domain.Models.Tables;
using Chat.Helpers;
using Chat.Domain.Models.ViewModels;

namespace Chat.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController()
        {
            _loginService = new LoginService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email))
                ModelState.AddModelError("email", "Campo obrigatório");

            if (string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("password", "Campo obrigatório");

            UserViewModel userVModel = null;
            if (ModelState.IsValid)
            {
                password = password != null ? EncryptionHelper.GetMD5FromString(password).ToLower() : password;

                bool b = _loginService.IsUserValid(email, password);
                if (b)
                {
                    userVModel = _loginService.GetUser(email);
                    HttpContext.Session.SetJsonSesssion<UserViewModel>("user", userVModel);
                    return RedirectToAction("Index", "Chat");
                }
            }

            return View("Index");
        }


        public IActionResult Logout()
        {

            User user = HttpContext.Session.GetJsonSession<User>("user");
            if (user != null)
            {
                HttpContext.Session.Remove("user");
            }

            return View("Index");
        }

    }
}
