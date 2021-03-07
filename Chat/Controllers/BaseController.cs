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
            UserViewModel user_session = HttpContext.Session.ExistJsonSession(key)
                ? HttpContext.Session.GetJsonSession<UserViewModel>(key)
                : null;

            bool is_valid = _service.IsUserValid(user_session);

            if (!is_valid)
            {
                context.Result = new RedirectResult("/Login/Index");
            }

            base.OnActionExecuting(context);
        }
    }
}
