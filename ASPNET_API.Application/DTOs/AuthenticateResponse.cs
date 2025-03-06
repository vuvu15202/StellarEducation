using ASPNET_API.Domain.Entities;
namespace ASPNET_API.Application.DTOs;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    public User UserInfo { get; set; }
    public string RedirectUrl { get; set; }
    public List<Role> Roles { get; set; }
    public string JwtToken { get; set; }
    public string AccessToken { get; set; }

    public AuthenticateResponse(User user, List<Role> role, string token)
    {
        //Id = user.UserId;
        //FirstName = user.FirstName;
        //LastName = user.LastName;
        //UserName = user.UserName;
        UserInfo = user;
        Roles = role;
        JwtToken = token;
        AccessToken = token;
        if (role.FirstOrDefault()!.RoleName.Contains("ADMIN")) RedirectUrl = "/admin/dashboard";
        else if (role.FirstOrDefault()!.RoleName.Contains("LECTURER")) RedirectUrl = "/admin/Courses";
        else if (role.FirstOrDefault()!.RoleName.Contains("STAFF")) RedirectUrl = "/admin/StudentFee";
        else RedirectUrl = "/";
    }

    public AuthenticateResponse(User user)
    {
        Id = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        UserName = user.UserName;
        RedirectUrl = "/auth/login";
    }
}