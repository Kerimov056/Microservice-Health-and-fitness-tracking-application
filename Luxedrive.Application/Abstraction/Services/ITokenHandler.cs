using Activictiy.Application.DTOs.Auth;
using Activictiy.Domain.Entitys.Identity;

namespace Activictiy.Application.Abstraction.Services;

public interface ITokenHandler
{
    public Task<TokenResponseDTo> CreateAccesToken(int minutes, int refreshTokenMinustes, AppUser appUser);
}
