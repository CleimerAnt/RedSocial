using Microsoft.AspNetCore.Mvc;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Application.Viewmodel.UserViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Persitence.Context;

namespace RedSocial.Controllers
{
    public class FriendsController : Controller
    {
        private readonly IFriendsServices _friendsService;
        private readonly IDboUserServices _userServices;
        private readonly ICommentServices _commentServices;
        public FriendsController(IFriendsServices friendsServices, IDboUserServices dboUserServices, ICommentServices commentServices)
        {
            _friendsService = friendsServices;
            _userServices = dboUserServices;
            _commentServices = commentServices; 
          
        }
        public async Task <IActionResult> Index(string userId)
        {
           var user = await _userServices.GetForIdentityId(userId); 

            var friends = await _friendsService.GetFriends(user.Id); 

            foreach (var uten in friends)
            {
                foreach(var iten in uten.PublicationList)
                {
                    iten.comments = await _commentServices.GetCommentByPublication(iten.Id);
                }
            }

            return View(friends);
        }

        public async Task<IActionResult> Create()
        {

            var friend = await _userServices.GetAllAsync();
            
            ViewBag.friend = friend;

            return View(new FrinedsPostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FrinedsPostViewModel friendsVm, string userIde)
        {
            var user = await _userServices.GetForIdentityId(userIde);
            var modelo = await _friendsService.GetFriends(user.Id);
            var modelFriend = await _userServices.GetById(friendsVm.FriendId);
            

            if (!ModelState.IsValid)
            {
                var friend = await _userServices.GetAllAsync();
                ViewBag.friend = friend;
                return View();  
            }

            friendsVm.UserId = user.Id;
            friendsVm.FriendFirstName = modelFriend.FirstName;  
            friendsVm.FriendUserName = modelFriend.UserName;
            friendsVm.FriendImgUrl = modelFriend.ImgUrl;
            friendsVm.FriendLastName = modelFriend.LastName;

            if(friendsVm.FriendUserName == user.UserName)
            {
                ModelState.AddModelError("Add Friend verification", "You Can't Add Yourself");
                var friend = await _userServices.GetAllAsync();
                ViewBag.friend = friend;
                return View();

            }

            foreach(var iten in modelo)
            {
                if(iten.FriendId == friendsVm.FriendId)
                {
                    ModelState.AddModelError("Friemd Register", "Friend already Registered");
                    var friend = await _userServices.GetAllAsync();
                    ViewBag.friend = friend;
                    return View();
                  
                }
            }

            await _friendsService.AddAsync(friendsVm);

            
            return RedirectToAction("Index", new {userId = userIde });  
        }

        public async Task<IActionResult> Delete()
        {
            return View();  
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int Id, string userId)
        {

            await _friendsService.Eliminar(Id);

            return RedirectToAction("Index", new { userId = userId });
        }
    }
}
