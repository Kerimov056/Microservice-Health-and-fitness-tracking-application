using Activictiy.Application.Abstraction.Services;
using Activictiy.Application.DTOs.Auth;
using Activictiy.Domain.Entitys.Common;
using Activictiy.Domain.Entitys.Identity;
using Activictiy.Domain.Helpers;
using Activictiy.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Activictiy.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly SignInManager<AppUser> _siginManager;
    private readonly IEmailService _emailService;
    private readonly AppDbContext _appDbContext;

    public AuthController(IAuthService authService, 
                          SignInManager<AppUser> siginManager,
                          IEmailService emailService,
                          AppDbContext appDbContext)
    {
        _authService = authService;
        _siginManager = siginManager;
        _emailService = emailService;
        _appDbContext = appDbContext;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
    {
        ArgumentNullException.ThrowIfNull(registerDTO, ExceptionResponseMessages.ParametrNotFoundMessage);

        SignUpResponse response = await _authService.Register(registerDTO)
                ?? throw new InvalidException(ExceptionResponseMessages.NotFoundMessage);

        if (response.Errors != null)
        {
            if (response.Errors.Count > 0)
            {
                return BadRequest(response.Errors);
            }
        }
        else
        {
            string subject = "Register Confirmation";
            string html = string.Empty;
            string password = registerDTO.password;

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "templates", "verify.html");
            html = System.IO.File.ReadAllText(filePath);

            html = html.Replace("{{password}}", password);

            _emailService.sendEmail(registerDTO.email, subject, html);

        }
        return Ok(response);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
    {
        var responseToken = await _authService.Login(loginDTO);
        return Ok(responseToken);
    }

    [HttpPost("AdminLogin")]
    public async Task<IActionResult> LoginAdmin([FromBody] LoginDTO loginDTO)
    {
        var responseToken = await _authService.LoginAdmin(loginDTO);
        return Ok(responseToken);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken([FromQuery] string ReRefreshtoken)
    {
        var response = await _authService.ValidRefleshToken(ReRefreshtoken);
        return Ok(response);
    }

    [HttpPost("password-reset")]
    public async Task<IActionResult> PasswordReset(string email)
    {
        await _authService.PasswordResetAsync(email);
        return Ok();
    }

    [HttpPost("ConfiremPassword")]
    public async Task<IActionResult> ConfiremPassword([FromForm] ResetPassword resetPassword)
    {
        await _authService.ResetPassword(resetPassword);
        return Ok();
    }

    [HttpPost("AdminCreate")]
    public async Task<IActionResult> AdminCreate([FromQuery] string superAdminId, [FromQuery] string appUserId)
    {
        await _authService.AdminCreate(superAdminId, appUserId);
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPost("AdminDelete")]
    public async Task<IActionResult> AdminDelete([FromQuery] string superAdminId, [FromQuery] string appUserId)
    {
        await _authService.AdminDelete(superAdminId, appUserId);
        return Ok();
    }

    [HttpGet("AllAdmin")]
    public async Task<IActionResult> AllAdminUsers([FromQuery] string? searchUser)
    {
        var adminUsers = await _authService.AllAdminUser(searchUser);
        return Ok(adminUsers);
    }


    [HttpGet("AllMember")]
    public async Task<IActionResult> AllMemberUssers([FromQuery] string? searchUser)
    {
        var memberUsers = await _authService.AllMemberUser(searchUser);
        return Ok(memberUsers);
    }

    [HttpGet("ByAdmin")]
    public async Task<IActionResult> GetSuperAdmin()
    {
        var SuperAdmin = await _authService.ByAdmin();
        return Ok(SuperAdmin);
    }


    [HttpGet("ByUser")]
    public async Task<IActionResult> ByUser([FromQuery] string? userId)
    {
        var Users = await _authService.ByUser(userId);
        return Ok(Users);
    }


    [HttpDelete("RemoveUser")]
    public async Task<IActionResult> UserRemove([FromQuery] string superAdminId, [FromQuery] string userId)
    {
        await _authService.RemoveUser(superAdminId, userId);
        return Ok();
    }

}
