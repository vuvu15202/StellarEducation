namespace ASPNET_API.Application.DTOs;

using System.ComponentModel.DataAnnotations;

public class AuthenticateRequest
{
    [Required(ErrorMessage = "Bạn chưa nhập tên tài khoản.")]
    public string UserName { get; set; }

    //[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",ErrorMessage = "Mật khẩu sai.")]
    [Required(ErrorMessage = "Bạn chưa nhập mật khẩu.")]
    public string Password { get; set; }
    public string? Email { get; set; }
}