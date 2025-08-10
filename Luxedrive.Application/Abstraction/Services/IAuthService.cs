using Activictiy.Application.Abstraction.Services.AdminCommands;
using Activictiy.Application.Abstraction.Services.SosicalAuthentications;
using Activictiy.Application.DTOs.Auth;
using Activictiy.Domain.Entitys.Identity;
using Activictiy.Domain.Helpers;

namespace Activictiy.Application.Abstraction.Services;

public interface IAuthService: IExternalAuthentications, IAdminCommands
{
    Task<SignUpResponse> Register(RegisterDTO registerDTO);
    Task<TokenResponseDTo> Login(LoginDTO loginDTO);
    Task<TokenResponseDTo> LoginAdmin(LoginDTO loginDTO);
    Task<TokenResponseDTo> ValidRefleshToken(string refreshToken);
    Task PasswordResetAsync(string email);
    Task<bool> ResetPassword(ResetPassword resetPassword);
    Task<string> ByAdmin();
}
