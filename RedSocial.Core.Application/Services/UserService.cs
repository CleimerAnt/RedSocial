using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Interfaces.Account;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;
using RedSocial.Core.Application.Viewmodel.FriendsViewModel;
using RedSocial.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IAccountServices _accountServices;
        private readonly IMapper _mapper;
        private readonly IDboUserServices _DbouserServices;
        private readonly IFriendsServices _friendsServices;
        
        public UserService(IAccountServices accountServices, IMapper mapper, IDboUserServices dboUserServices, IFriendsServices friendsServices)
        {
            _accountServices = accountServices;
            _mapper = mapper;
           _DbouserServices = dboUserServices;  
           _friendsServices = friendsServices;
        }

        
        public async Task UpdateUserIdentity(UserPostViewModel vm)
        {

            var userDb = await _DbouserServices.GetForIdentityId(vm.Id);

            var friends = await _friendsServices.GetFriendByFriendId(userDb.Id);

            vm.ImgUrl =  await _accountServices.EditarImg(vm.file, vm.Id, userDb.ImgUrl);

            if (friends is not null)
            {

                FrinedsPostViewModel friend = new();
                friend.FriendFirstName = vm.FirstName;
                friend.FriendLastName = vm.LastName;
                friend.FriendUserName = friends.FriendUserName;
                friend.FriendId = friends.FriendId;
                friend.FriendImgUrl = vm.ImgUrl;
                friend.UserId = friends.UserId;
                friend.Id = friends.Id;

                await _friendsServices.Editar(friend, friend.Id);
            }


            dbUserPostViewModel userPost = new();
            userPost.Id = userDb.Id;
            userPost.UserName = userDb.UserName; 
            userPost.Email = vm.Email;
            userPost.LastName = vm.LastName;
            userPost.FirstName = vm.FirstName;
            userPost.PhoneNumber = vm.PhoneNumber;
            userPost.LastName = vm.LastName;
            userPost.PassWord = vm.PassWord;
            userPost.UserIdIndentity = vm.Id;
            userPost.ImgUrl = vm.ImgUrl;
            await _DbouserServices.Editar(userPost, userPost.Id);

            await _accountServices.UpdateUser(vm);

        }
        public async Task<RegistrerResponse> RegisterAsync(UserPostViewModel vm, string origin)
        {
            

            RegistrerRequest registerRequest = _mapper.Map<RegistrerRequest>(vm);


            return await _accountServices.RegistrerBasicUserAsync(registerRequest, origin);

  
        }

        public async Task<string> ConfirmEmailAsync(string userId, string token)
        {

            return await _accountServices.ConfirmAccountAsync(userId, token);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin)
        {

            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _accountServices.ForgotPasswordAsync(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm)
        {

            ResetPasswordRequest forgotRequest = _mapper.Map<ResetPasswordRequest>(vm);

            return await _accountServices.ResetPasswordAsync(forgotRequest);
        }

        public async Task SignOutAsync()
        {

            await _accountServices.SingOutAsync();
        }
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel LoginVm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(LoginVm);

            AuthenticationResponse userResponse = await _accountServices.AuthenticateASYNC(loginRequest);


            return userResponse;
        }

       
    }
}
