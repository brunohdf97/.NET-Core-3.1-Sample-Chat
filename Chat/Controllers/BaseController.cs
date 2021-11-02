using Chat.Domain.Models.ViewModels;
using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            LoginService _service = new LoginService();

            string key = "user";
            UserViewModel userSession = HttpContext.Session.ExistJsonSession(key)
                ? HttpContext.Session.GetJsonSession<UserViewModel>(key)
                : null;

            bool isValid = _service.IsUserValid(userSession);

            if (!isValid)
            {
                context.Result = new RedirectResult("/Login/Index");
            }

            base.OnActionExecuting(context);
        }
    }
}
