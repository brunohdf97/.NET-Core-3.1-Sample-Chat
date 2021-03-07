using Microsoft.AspNetCore.Mvc;

namespace Chat.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
