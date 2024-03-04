﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Dtos.Email.Account;
using RedSocial.Core.Application.Enums;
using RedSocial.Core.Application.Interfaces.Email;
using RedSocial.Infraestructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedSocial.Infraestructure.Identity.Services
{
    public class AccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest)
        {
            AuthenticationResponse response = new();

            var user = await _userManager.FindByEmailAsync(requuest.Email);

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

            };

            var result = await _userManager.CreateAsync(user, request.PassWord);
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

            var route = "Usuarios/ConfirmEmail";

            var Uri = new Uri(string.Concat($"{origin}/", route));

            var verificationUrL = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrL = QueryHelpers.AddQueryString(verificationUrL, "token", code);

            return verificationUrL;
        }
        public void probarImplementr()
        {

        }
        private async Task<string> SendVeForgotPassWord(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "Usuarios/ResetPassword";

            var Uri = new Uri(string.Concat($"{origin}/", route));


            var verificationUrL = QueryHelpers.AddQueryString(Uri.ToString(), "token", code);

            return verificationUrL;
        }
    }
}