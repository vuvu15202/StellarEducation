using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ASPNET_MVC.Models.Entity
{
    public partial class UserRole
    {
        public int UserRoleId { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;
        [JsonIgnore]
        public virtual User User { get; set; } = null!;
    }
}
