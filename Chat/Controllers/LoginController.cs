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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string password)
        {
            LoginService login_service = new LoginService();

            if (string.IsNullOrWhiteSpace(email))
                ModelState.AddModelError("email", "Campo obrigatório");

            if (string.IsNullOrWhiteSpace(password))
                ModelState.AddModelError("password", "Campo obrigatório");

            UserViewModel user_vmodel = null;
            if (ModelState.IsValid)
            {
                password = password != null ? EncryptionHelper.GetMD5FromString(password).ToLower() : password;

                bool b = login_service.IsUserValid(email, password);
                if (b)
                {
                    user_vmodel = login_service.GetUser(email);
                    HttpContext.Session.SetJsonSesssion<UserViewModel>("user", user_vmodel);
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
