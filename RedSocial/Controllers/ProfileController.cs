using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProfileController(IUserService userService, IDboUserServices dboUserServices, IMapper mapper)
        {
            _userService = userService;
            _DbouserServices = dboUserServices;
            _mapper = mapper;
        }
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
           
         
             return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UserPostViewModel userVm)
        {
            if (userVm != null)
            {
              

                await _userService.UpdateUserIdentity(userVm);
              
            }

            return View("Index");
        }
    }
}
