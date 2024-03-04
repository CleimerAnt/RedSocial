using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Helpers;
using RedSocial.Middlewares;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;

namespace RedSocial.Controllers
{
    public class UserController : Controller
    {
       
        private readonly IUserService _userService;
     
     
        public UserController(IUserService userService )
        {
            _userService = userService;
          

        }
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel LoginVm)
        {
            if (!ModelState.IsValid)
            {
                return View(LoginVm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(LoginVm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.set<AuthenticationResponse>("User", userVm);
                var userId = userVm.Id;
                return RedirectToAction("Index", "Publications", new { userId = userId});

            }
            else
            {
                LoginVm.HassError = userVm.HasError;
                LoginVm.Error = userVm.Error;
                return View(LoginVm);
            }

        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Registrar()
        {
            return View(new UserPostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(UserPostViewModel User)
        {
            if (!ModelState.IsValid)
            {
                return View(User);
            }

            var origin = Request.Headers["origin"];

            RegistrerResponse response = await _userService.RegisterAsync(User, origin);

            /*  if (User != null )
              {
                    User.ImgUrl = UploadFile(User.file, User.Id);

                    await _serviceUser.Editar(User, User.Id);
              }*/


            if (response.HasError)
            {
                User.HasError = response.HasError;
                User.Error = response.Error;
                return View(User);
            }

            return RedirectToAction("Index", "User");
        }


        public async Task<IActionResult> CerrarSesion()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("User");

            return RedirectToAction("Index", "User");
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);

            return View("ConfirmEmail", response);
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _userService.ForgotPasswordAsync(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToAction("Index", "User");
        }
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }


            ResetPasswordResponse response = await _userService.ResetPasswordAsync(vm);

            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }

            return RedirectToAction("Index", "User");

        }

        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }

       

    }
}
