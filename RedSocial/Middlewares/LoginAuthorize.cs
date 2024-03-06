using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RedSocial.Core.Application.Helpers;
using RedSocial.Controllers;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;

namespace RedSocial.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;
        private readonly IHttpContextAccessor _contextAccessor;

        public LoginAuthorize(ValidateUserSession validateUserSession, IHttpContextAccessor contextAccessor)
        {
            _userSession = validateUserSession;
            _contextAccessor = contextAccessor;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var user = _contextAccessor.HttpContext.Session.get<AuthenticationResponse>("User");

                var controller = (UserController)context.Controller;

                context.Result = controller.RedirectToAction("Index", "Publications", new {userId = user.Id });
            }
            else
            {
                await next();
            }
        }
    }
}
