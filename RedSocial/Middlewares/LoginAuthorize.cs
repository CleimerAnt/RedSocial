using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RedSocial.Controllers;

namespace RedSocial.Middlewares
{
    public class LoginAuthorize : IAsyncActionFilter
    {
        private readonly ValidateUserSession _userSession;

        public LoginAuthorize(ValidateUserSession validateUserSession)
        {
            _userSession = validateUserSession;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (_userSession.HasUser())
            {
                var controller = (PublicationsController)context.Controller;

                context.Result = controller.RedirectToAction("Index", "Publications");
            }
            else
            {
                await next();
            }
        }
    }
}
