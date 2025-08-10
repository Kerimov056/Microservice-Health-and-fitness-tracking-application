namespace Activictiy.Application.DTOs.Auth;

public record TokenResponseDTo(string token,
                               DateTime expireDate,
                               DateTime refreshTokenExpration,
                               string refreshToken,
                               string username,
                               string email,
                               string appUserId);