using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Viewmodel.ReplyViewModel;
using RedSocial.Infraestructure.Persitence.Repositories;

namespace RedSocial.Controllers
{
    public class ReplyController : Controller
    {
        private readonly IReplyServices _replyServices;
        private readonly IDboUserServices _userServices;
        public ReplyController(IReplyServices services, IDboUserServices userService)
        {
            _replyServices = services;
            _userServices = userService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string text, int commentId, string UserIdentity, bool IsInFried)
        {
            var user = await _userServices.GetForIdentityId(UserIdentity);
            
            ReplyPostViewModel reply = new();
            reply.Text = text;  
            reply.UserId = user.Id;  
            reply.comentId = commentId;
          

           await  _replyServices.AddAsync(reply);

            if (IsInFried == true)
            {
                return RedirectToAction("Index", "Friends", new { userId = UserIdentity });
            }

            return RedirectToAction("Index", "Publications", new { userId = UserIdentity });
        }
    }
}
