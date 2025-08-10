using Microsoft.AspNetCore.Identity;

namespace Activictiy.Domain.Entitys.Identity;

public class AppUser : IdentityUser
{
    public bool IsActive { get; set; }
    public string? FullName { get; set; }
    public DateTime RefreshTokenExpration { get; set; }
    public string? RefreshToken {  get; set; }
    public DateTime? BirthDate { get; set; }
    public List<CarComment>? carComments { get; set; }

}
