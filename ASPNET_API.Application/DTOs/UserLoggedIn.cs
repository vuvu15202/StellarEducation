namespace ASPNET_API.Domain.Entities
{
    public class UserLoggedIn
    {
        public User? UserInfo { get; set; } = null!;
        public string? AccessToken { get; set; } = null!;
        public List<int>? Roles { get; set; }

    }
}
