using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CodeActions;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;

namespace RedSocial.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly  IDboUserServices _DbouserServices;
        private readonly IFriendsServices _friendsServices;
        private readonly IMapper _mapper;
        public ProfileController(IUserService userService, IDboUserServices dboUserServices, IMapper mapper, IFriendsServices friendsServices)
        {
            _userService = userService;
            _DbouserServices = dboUserServices;
            _mapper = mapper;
            _friendsServices = friendsServices;

        }
        [Authorize]
        public async Task  <IActionResult> Index(string userIde)
        {
            var userDb = await _DbouserServices.GetForIdentityId(userIde);

            UserPostViewModel user = new();
            user.ImgUrl = userDb.ImgUrl;
            user.PhoneNumber = userDb.PhoneNumber;
            user.UserName = userDb.UserName;    
            user.Email = userDb.Email;
            user.LastName = userDb.LastName;
            user.FirstName = userDb.FirstName;

            EditUserViewModel userModel = _mapper.Map<EditUserViewModel>(user);
         
             return View(userModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Update(EditUserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",user);    
            }

            UserPostViewModel userVm = _mapper.Map<UserPostViewModel>(user);

            if (userVm != null)
            {
                await _userService.UpdateUserIdentity(userVm);
            }

            return RedirectToAction("Index", "Publications" ,new { userId = userVm.Id });
        }
    }
}
