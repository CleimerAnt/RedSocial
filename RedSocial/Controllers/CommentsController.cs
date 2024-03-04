using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Viewmodel.CommentsViewModel;

namespace RedSocial.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentServices _commentsServices;
        private readonly IDboUserServices _userServices;
        public CommentsController(ICommentServices commentServices,IDboUserServices userServices )
        {
            _commentsServices = commentServices;
            _userServices = userServices;   
        }

       
        public async Task <IActionResult> IndexComment()
        {
          

            return View();
        }
       /* [HttpPost]
        public async Task<IActionResult> IndexComment(int Id)
        {
            var comments = await _commentsServices.GetCommentByPublication(Id);

            return View(comments);
        }*/
        

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string Text, string identityId, int publicationId, bool IsInFried)
        {
           var user = await _userServices.GetForIdentityId(identityId); 
            CommentsPostViewModel commentsVm = new();
            commentsVm.Text = Text;
            commentsVm.UserId = user.Id;
            commentsVm.PublicationId = publicationId;

            await _commentsServices.AddAsync(commentsVm);

            if(IsInFried == true)
            {
                return RedirectToAction("Index", "Friends", new { userId = identityId });   
            }

            return RedirectToAction("Index", "Publications" , new { userId = identityId });
        }
    }
}
