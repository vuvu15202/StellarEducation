using ASPNET_API.Domain.Entities;
using System.Text.Json.Serialization;
namespace ASPNET_API.Application.DTOs { 
    public class UserDTO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public DateTime? EnrollDate { get; set; }
        public string? Image { get; set; }
        public string? Description { get; set; }


        public List<RoleEnum> Roles { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}