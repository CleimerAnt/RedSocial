using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;

namespace RedSocial.Core.Application.Interfaces.Account
{
    public interface IAccountServices
    {
        Task<AuthenticationResponse> AuthenticateASYNC(AuthenticationRequest requuest);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<RegistrerResponse> RegistrerBasicUserAsync(RegistrerRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SingOutAsync();
      
    }
}