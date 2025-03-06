namespace ASPNET_MVC.Models.Entity
{
    public class UserLoggedIn
    {
        public User UserInfo { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public List<int> Roles { get; set; }
    }
}
