namespace Activictiy.Application.DTOs.Auth;

public record RegisterDTO(string? fullName, string userName, string email, string password, DateTime? birthDate);
