using Chat.Domain.Models.ViewModels;
using Chat.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    public class ChatController : BaseController
    {
        public IActionResult Index()
        {
            UserViewModel user = HttpContext.Session.GetJsonSession<UserViewModel>("user");
            return View(user);
        }
    }
}
