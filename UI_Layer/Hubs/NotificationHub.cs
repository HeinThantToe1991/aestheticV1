using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using System.Text.Json.Serialization.Metadata;
using Newtonsoft.Json;
using UI_Layer.Areas.BackendSystem.Models;
using UI_Layer.Data;
using UI_Layer.Data.AppData;
using UI_Layer.Helpers;
using UI_Layer.Models;

namespace UI_Layer.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IDistributedCache _cache;
        public NotificationHub(ApplicationDbContext dbContext, IDistributedCache cache)
        {
            this._dbContext = dbContext;
            this._cache = cache;
        }

        public async Task SendNotificationToAll(string message)
        {
            await Clients.All.SendAsync("ReceivedNotification", message);
        }

        //public async Task SendNotificationToClient(string message, string username)
        //{
        //    var hubConnections = dbContext.HubConnections.Where(con => con.Username == username).ToList();
        //    foreach (var hubConnection in hubConnections)
        //    {
        //        await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
        //    }
        //}
        //public async Task SendNotificationToGroup(string message, string group)
        //{
        //    var hubConnections = dbContext.HubConnections.Join(dbContext.TblUser, c => c.Username, o => o.Username, (c, o) => new { c.Username, c.ConnectionId, o.Dept }).Where(o => o.Dept == group).ToList();
        //    foreach (var hubConnection in hubConnections)
        //    {
        //        string username = hubConnection.Username;
        //        await Clients.Client(hubConnection.ConnectionId).SendAsync("ReceivedPersonalNotification", message, username);
        //        //Call Send Email function here
        //    }
        //}

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("OnConnected");
            return base.OnConnectedAsync();
        }

        public async Task SaveUserConnection()
        {
            var connectionId = Context.ConnectionId;
            var logInView =  await _cache.GetStringAsync("LoggedInUser"); ;
            var logInUser = JsonConvert.DeserializeObject<CustomerViewModel>(logInView);
            ActiveUserDM activeUser = new ActiveUserDM()
            {
                Id = Guid.NewGuid(),
                ConnectionId = connectionId,
                UserId = Guid.Parse(logInUser.Id),
                UserType =logInUser.LogInUserStatus,
                CreatedDate = DateTime.Now,
                CreatedUserId = Guid.Parse(logInUser.Id)
            };
            _dbContext.ActiveUsers.Add(activeUser);
            await _dbContext.SaveChangesAsync();
            //await Clients.All.SendAsync("UpdateActiveUsers", "Connected HTT");
            UpdateActiveUsers();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var hubConnection = _dbContext.ActiveUsers.FirstOrDefault(con => con.ConnectionId == Context.ConnectionId);
            if (hubConnection != null)
            {
                _dbContext.ActiveUsers.Remove(hubConnection);
                await _dbContext.SaveChangesAsync();

            }
             await base.OnDisconnectedAsync(exception);
             UpdateActiveUsers();
        }

        private async Task UpdateActiveUsers()
        {
            var activeUsers = _dbContext.ActiveUsers
                .GroupBy(user => user.UserType)
                .Select(group => new
                {
                    UserType = group.Key.Equals(Constant.UserType.Staffs) ? "Staff" :
                        group.Key.Equals(Constant.UserType.Doctors) ? "Doctor" : "Customer",
                    UserCount = group.Count()
                })
                .ToList();
            await Clients.All.SendAsync("UpdateActiveUsers", JsonConvert.SerializeObject(activeUsers));
        }

        public void ClearActiveUsers()
        {
            _dbContext.ActiveUsers.RemoveRange(_dbContext.ActiveUsers);
            _dbContext.SaveChanges();
        }
        //public async Task Disconnect()
        //{
        //    // Perform any cleanup or additional logic here
        //    await Clients.Caller.SendAsync("OnDisconnected");
        //    await OnDisconnectedAsync(null); // Manually call OnDisconnectedAsync
        //}

    }
}
