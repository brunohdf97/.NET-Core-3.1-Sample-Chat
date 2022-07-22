using Chat.Domain.Models.DTOS;
using Chat.Domain.Models.ViewModels;
using Chat.Extensions;
using Chat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Chat.Controllers
{
    public class BaseController : Controller
    {
        private readonly LoginService _loginService;
        public BaseController()
        {
            _loginService = new LoginService();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string key = "user";
            UsuarioLogadoDTO userSession = HttpContext.Session.ExistJsonSession(key)
                ? HttpContext.Session.GetJsonSession<UsuarioLogadoDTO>(key)
                : null;

            bool isValid = _loginService.IsUserValid(userSession);

            if (!isValid)
            {
                context.Result = new RedirectResult("/Login/Index");
            }

            base.OnActionExecuting(context);
        }
    }
}
