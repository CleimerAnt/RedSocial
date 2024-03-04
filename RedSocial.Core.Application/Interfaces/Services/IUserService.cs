using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Viewmodel.AccounsViewModel;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel LoginVm);
        Task<RegistrerResponse> RegisterAsync(UserPostViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel vm);
        Task SignOutAsync();

        Task UpdateUserIdentity(UserPostViewModel vm);
    }
}