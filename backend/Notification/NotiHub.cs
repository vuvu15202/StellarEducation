using Microsoft.AspNetCore.SignalR;

namespace ASPNET_API.Notification
{
    public class NotiHub : Hub
    {
        //public async Task SendMess(string user, string message)
        //{
        //    //await Clients.All.SendAsync("ReceivedMess", user, message);
        //    User u = UserList.GetUser(user);
        //    User caller = UserList.GetUserByConnectionId(Context.ConnectionId);
        //    if (u is null) return;
        //    //await Clients.All.SendAsync("ReceivedMess", caller.UserName, message);
        //    await Clients.Client(u.ConnectionId).SendAsync("ReceivedMess", caller.UserName, message);
        //    //await Clients.Caller.SendAsync("ReceivedMess", message);
        //}

        public void SetUserId(string userId)
        {
            UserList.AddUser(new UserNoti(userId, Context.ConnectionId));
        }
    }
}
