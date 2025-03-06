namespace ASPNET_API.Notification
{
    public class UserNoti
    {
        public string UserName { get; set; }
        public string ConnectionId { get; set; }

        public UserNoti(string userName, string connectionId)
        {
            UserName = userName;
            ConnectionId = connectionId;
        }
    }

    public class UserList
    {
        public static List<UserNoti> Users = new List<UserNoti>();

        public static void AddUser(UserNoti user)
        {
            if (Users.SingleOrDefault(u => u.UserName.Equals(user.UserName)) != null)
            {
                int index = Users.FindIndex(u => u.UserName.Equals(user.UserName));
                Users[index].ConnectionId = user.ConnectionId;  
            }
            else
            {
                Users.Add(user);
            }
        }

        public static UserNoti GetUser(string userName)
        {
            return Users.FirstOrDefault(x => x.UserName.Equals(userName));
        }

        public static UserNoti GetUserByConnectionId(string connectionId)
        {
            return Users.FirstOrDefault(x => x.ConnectionId.Equals(connectionId));
        }
    }
}
