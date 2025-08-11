using Contracts.Events;
using Activictiy.Application.Abstraction.Services;
using Activictiy.Application.DTOs.Auth;
using Activictiy.Domain.Entitys.Common;
using Activictiy.Domain.Entitys.Identity;
using Activictiy.Domain.Enums.Role;
using Activictiy.Domain.Helpers;
using Activictiy.Persistance.Context;
using Activictiy.Persistance.Exceptions;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Activictiy.Persistance.Implementations.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly ITokenHandler _tokenHandler;
    private readonly IEmailService _emailService;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuthService(UserManager<AppUser> userManager,
                       SignInManager<AppUser> siginManager,
                       RoleManager<IdentityRole> roleManager,
                       AppDbContext context, IConfiguration configuration,
                       ITokenHandler tokenHandler, IEmailService emailService,
                       IPublishEndpoint publishEndpoint)
    {
        _userManager = userManager;
        _siginManager = siginManager;
        _roleManager = roleManager;
        _context = context;
        _configuration = configuration;
        _tokenHandler = tokenHandler;
        _emailService = emailService;
        _publishEndpoint = publishEndpoint;
    }

    public async Task AdminCreate(string superAdminId, string appUserId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetUser = await _userManager.FindByIdAsync(appUserId);

        if (targetUser == null) throw new NotFoundException("User Not Found");

        await _userManager.RemoveFromRoleAsync(targetUser, "Member");
        await _userManager.AddToRoleAsync(targetUser, "Admin");

    }

    public async Task AdminDelete(string superAdminId, string appAdminId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");


        var targetAdmin = await _userManager.FindByIdAsync(appAdminId);

        if (targetAdmin == null) throw new NotFoundException("Admin Not Found");

        await _userManager.RemoveFromRoleAsync(targetAdmin, "Admin");
        await _userManager.AddToRoleAsync(targetAdmin, "Member");

    }

    public async Task<List<AppUser>> AllAdminUser(string? searchUser)
    {
        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (adminRole == null) return new List<AppUser>();

        var query = from user in _context.Users
                    join userRole in _context.UserRoles on user.Id equals userRole.UserId
                    where userRole.RoleId == adminRole.Id
                    select user;

        if (!string.IsNullOrEmpty(searchUser))
        {
            query = query.Where(x =>
                x.FullName.ToLower().Contains(searchUser.ToLower()) ||
                x.Email.ToLower().Contains(searchUser.ToLower()));
        }

        return await query.ToListAsync();
    }

    public async Task<List<AppUser>> AllMemberUser(string? searchUser)
    {
        var adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Member");
        if (adminRole == null) return new List<AppUser>();

        var query = from user in _context.Users
                    join userRole in _context.UserRoles on user.Id equals userRole.UserId
                    where userRole.RoleId == adminRole.Id
                    select user;

        if (!string.IsNullOrEmpty(searchUser))
        {
            query = query.Where(x =>
                x.FullName.ToLower().Contains(searchUser.ToLower()) ||
                x.Email.ToLower().Contains(searchUser.ToLower()));
        }

        return await query.ToListAsync();
    }

    public async Task<string> ByAdmin()
    {
        var users = await _context.Users.ToListAsync();
        foreach (var item in users)
        {
            var userRoles = await _userManager.GetRolesAsync(item);
            if (userRoles.Contains("SuperAdmin")) return item.Id.ToString();
        }
        throw new NullReferenceException();
    }

    public async Task<AppUser> ByUser(string? userId)
    {
        var byUser = await _userManager.FindByIdAsync(userId);
        if (byUser is null) throw new NotFoundException("SuperAdmin Not found");
        return byUser;
    }

    public async Task<TokenResponseDTo> Login(LoginDTO loginDTO)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.usernameOrEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(loginDTO.usernameOrEmail);
            if (appUser is null) throw new LogInFailerException("Invalid Login!");
        }

        SignInResult signResult = await _siginManager.CheckPasswordSignInAsync(appUser, loginDTO.password, true);
        if (!signResult.Succeeded)
        {
            throw new LogInFailerException("Invalid Login!");
        }

        var tokenResponse = await _tokenHandler.CreateAccesToken(2,3, appUser);
        appUser.RefreshToken = tokenResponse.refreshToken;
        appUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
        await _userManager.UpdateAsync(appUser);
        return tokenResponse;
    }

    public async Task<TokenResponseDTo> LoginAdmin(LoginDTO loginDTO)
    {
        AppUser appUser = await _userManager.FindByEmailAsync(loginDTO.usernameOrEmail);
        if (appUser is null)
        {
            appUser = await _userManager.FindByNameAsync(loginDTO.usernameOrEmail);
            if (appUser is null) throw new LogInFailerException("Invalid Login!");
        }

        SignInResult signResult = await _siginManager.CheckPasswordSignInAsync(appUser, loginDTO.password, true);
        if (!signResult.Succeeded) throw new LogInFailerException("Invalid Login!");


        var userRoles = await _userManager.GetRolesAsync(appUser);

        if (userRoles.Contains("Admin") || userRoles.Contains("SuperAdmin"))
        {
            var tokenResponse = await _tokenHandler.CreateAccesToken(2, 3, appUser);
            appUser.RefreshToken = tokenResponse.refreshToken;
            appUser.RefreshTokenExpration = tokenResponse.refreshTokenExpration;
            await _userManager.UpdateAsync(appUser);
            return tokenResponse;
        }
        else throw new LogInFailerException("You do not have permission to access this resource.");
    }

    public async Task PasswordResetAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user is null) throw new NotFoundException("Not Found");

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        byte[] tokenBytes = Encoding.UTF8.GetBytes(resetToken);
        resetToken = WebEncoders.Base64UrlEncode(tokenBytes);

        await _emailService.SendPasswordResetMailAsync(user.Email, user.Id, resetToken);
    }

    public async Task<SignUpResponse> Register(RegisterDTO registerDTO)
    {
        AppUser newUser = new()
        {
            FullName = registerDTO.fullName,
            UserName = registerDTO.userName,
            Email = registerDTO.email,
            BirthDate = registerDTO.birthDate,
        };

        IdentityResult identityResult = await _userManager.CreateAsync(newUser, registerDTO.password);
        if (!identityResult.Succeeded)
        {
            StringBuilder errorMessage = new();
            foreach (var error in identityResult.Errors) 
            {
                errorMessage.AppendLine(error.Description);
            }
            throw new RegistrationException(errorMessage.ToString());
        }

        var result = await _userManager.AddToRoleAsync(newUser, Role.Member.ToString());
        if (!result.Succeeded)
        {
            return new SignUpResponse
            {
                StatusMessage = ExceptionResponseMessages.UserFailedMessage,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
        //microservice event
        var @event = new UserCreatedEvent
        {
            UserId = Guid.Parse(newUser.Id),
            Email = newUser.Email,
            CreatedAt = DateTime.UtcNow
        };
        await _publishEndpoint.Publish(@event);


        return new SignUpResponse
        {
            Errors = null,
            StatusMessage = ExceptionResponseMessages.UserSuccesMessage,
            UserEmail = newUser.Email
        };
    }

    public async Task RemoveUser(string superAdminId, string userId)
    {
        var bySuperAdmin = await _userManager.FindByIdAsync(superAdminId);
        if (bySuperAdmin is null) throw new NotFoundException("SuperAdmin Not found");
        if (bySuperAdmin == null || !await _userManager.IsInRoleAsync(bySuperAdmin, "SuperAdmin"))
            throw new UnauthorizedAccessException("You do not have permission to perform this action.");

        var targetUser = await _userManager.FindByIdAsync(userId);
        if (targetUser == null) throw new NotFoundException("User Not Found");

        _context.Remove(targetUser);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ResetPassword(ResetPassword resetPassword)
    {
        AppUser user = await _userManager.FindByIdAsync(resetPassword.UserId);
        if (user is null) throw new NotFoundException("Not found User");
        IdentityResult identityResult = await _userManager.RemovePasswordAsync(user);
        if (!identityResult.Succeeded) throw new Exception("Not change password");
        identityResult = await _userManager.AddPasswordAsync(user, resetPassword.Password);
        if (!identityResult.Succeeded) throw new Exception("password Not change ");
        return true;
    }

    public Task<TokenResponseDTo> ValidRefleshToken(string refreshToken)
    {
        throw new NotImplementedException();
    }
}
