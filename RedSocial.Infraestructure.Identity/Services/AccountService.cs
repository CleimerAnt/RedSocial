using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Dtos.Email.Account;
using RedSocial.Core.Application.Enums;
using RedSocial.Core.Application.Interfaces.Account;
using RedSocial.Core.Application.Interfaces.Email;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Viewmodel;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity.Services
{
    public class AccountService : IAccountServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IDboUserServices dboUserServices;
    

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IDboUserServices userServices)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
            dboUserServices = userServices;

        }

        public async Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByNameAsync(requuest.UserName);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(requuest.UserName);
            }

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with {requuest.Email} ";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, requuest.PassWord, false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Invalid Credential For {requuest.Email} ";
                return response;
            }

            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Accound No Confirmed for {requuest.Email} ";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;

            return response;
        }
        public async Task SingOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<RegistrerResponse> RegistrerBasicUserAsync(RegistrerRequest request, string origin)
        {
            RegistrerResponse response = new()
            {
                HasError = false
            };

            var userWithSameUserName = await _userManager.FindByNameAsync(request.UserName);
            if (userWithSameUserName != null)
            {
                response.HasError = true;
                response.Error = $"username {request.UserName} is already Taken";

                return response;
            }


            var userWithSameEmail = await _userManager.FindByEmailAsync(request.Email);
            if (userWithSameEmail != null)
            {
                response.HasError = true;
                response.Error = $"username {request.Email} is already registrer";

                return response;
            }

            var user = new ApplicationUser
            {
                EmailConfirmed = false,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,

            };

            var result = await _userManager.CreateAsync(user, request.PassWord);

            if(user != null && user.Id != null )
            {
                user.ImgUrl = UploadFile(request.file, user.Id);
                await _userManager.UpdateAsync(user);
            }

            dbUserPostViewModel userVm = new();
            userVm.ImgUrl = user.ImgUrl;
            userVm.UserName = user.UserName;    
            userVm.Email = user.Email;
            userVm.UserIdIndentity = user.Id;
            userVm.PhoneNumber = user.PhoneNumber;
            userVm.FirstName = user.FirstName;
            userVm.LastName = user.LastName;
            userVm.PassWord = user.PasswordHash;  


            await dboUserServices.AddAsync(userVm);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

                var verificationUri = await SendVerificationEmailUrl(user, origin);
                await _emailService.SendAsync(new EmailRequest()
                {
                    To = user.Email,
                    Body = $"Please Confirm Your Account Visiting this Url {verificationUri}",
                    Subject = "Confirm registration"
                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"An Error ocurred trying to register the user";

                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccountAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return $"No Account Registrer with this User";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return $"Accound confirmed for {user.Email} You can now use the app";
            }
            else
            {
                return $"An Error ocurred while confirming {user.Email}";
            }
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new()
            {
                HasError = false
            };

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with ${request.Email}";
                return response;
            }

            var verificationUri = await SendVeForgotPassWord(account, origin);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"Please reset Your Account Visiting this Url {verificationUri}",
                Subject = "Reset Password"
            });

            return response;
        }
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new()
            {
                HasError = false
            };

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No Accounts registered with ${request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);

            await _emailService.SendAsync(new EmailRequest()
            {
                To = account.Email,
                Body = $"Your New Password is {request.Password}",
                Subject = "New Password"
            });

            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"An Error ocurred while reset password";
                return response;
            }



            return response;
        }
        private async Task<string> SendVerificationEmailUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ConfirmEmail";

            var Uri = new Uri(string.Concat($"{origin}/", route));

            var verificationUrL = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrL = QueryHelpers.AddQueryString(verificationUrL, "token", code);

            return verificationUrL;
        }

        
        private async Task<string> SendVeForgotPassWord(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ResetPassword";

            var Uri = new Uri(string.Concat($"{origin}/", route));


            var verificationUrL = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUrL;
        }
        public async Task UpdateUser(UserPostViewModel vm)
        {
            vm.PassWord = await UpdatePassword(vm.Id, vm.PassWord);

            var user = await _userManager.FindByIdAsync(vm.Id);
         
            user.UserName = vm.UserName;
            user.Email = vm.Email;
            user.PhoneNumber = vm.PhoneNumber;
            user.FirstName = vm.FirstName;
            user.LastName = vm.LastName;
            user.ImgUrl = vm.ImgUrl;
            user.PasswordHash = vm.PassWord;

            
             var resultado =   await _userManager.UpdateAsync(user);
           
            if (resultado.Succeeded) { }
        }

        public  async Task<string> EditarImg(IFormFile file, string Id, string  ImgUrl)
        {
            var img = "";
            img =  UploadFile(file, Id, true, ImgUrl);
            return img;
        }
        public async Task<string> UpdatePassword(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            var newPasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);

            return newPasswordHash;
        }


        private string UploadFile(IFormFile file, string Id, bool isEditMode = false, string imageURL = "")
        {
            if (isEditMode && file == null)
            {
                return imageURL;
            }

            //Get Directory Path
            string BasePath = $"/img/user/{Id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{BasePath}");

            //Create Folder if no exits
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }


            //GetFilePath
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImage = imageURL.Split("/");
                string olImageName = oldImage[^1];
                string completeImageOldPath = Path.Combine(path, olImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }

            };
            return $"{BasePath}/{fileName}";
        }

    }
}