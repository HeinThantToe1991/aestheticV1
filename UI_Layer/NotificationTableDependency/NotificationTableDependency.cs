using System.ComponentModel.DataAnnotations;
using UI_Layer.NotificationTableDependency;
using UI_Layer.Hubs;
using UI_Layer.Models;
using TableDependency.SqlClient;
using UI_Layer.Data.AppData;

namespace UI_Layer.NotificationTableDependency
{
    public class NotificationTableDependency : INotificationTableDependency
    {
        SqlTableDependency<NotificationsDM> tableDependency;
        NotificationHub notificationHub;

        public NotificationTableDependency(NotificationHub notificationHub)
        {
            this.notificationHub = notificationHub;
        }

        public void NotificationsTableDependency(string connectionString)
        {
            tableDependency = new SqlTableDependency<NotificationsDM>(connectionString);
            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();
        }

        public void TableDependency_OnError(object sender, global::TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Console.WriteLine($"{nameof(NotificationsDM)} SqlTableDependency error: {e.Error.Message}");
        }

        public async void TableDependency_OnChanged(object sender, global::TableDependency.SqlClient.Base.EventArgs.RecordChangedEventArgs<NotificationsDM> e)
        {
            if (e == null) throw new ArgumentNullException(nameof(e));
            if(e.ChangeType != global::TableDependency.SqlClient.Base.Enums.ChangeType.None)
            {
                var notification = e.Entity;
                if(notification.MessageType == "All")
                {
                    await notificationHub.SendNotificationToAll(notification.MessageText);
                }
                //else if(notification.MessageType == "Personal")
                //{
                //    await notificationHub.SendNotificationToClient(notification.Message, notification.Username);
                //}
                //else if (notification.MessageType == "Group")
                //{
                //    await notificationHub.SendNotificationToGroup(notification.Message, notification.Username);
                //}
            }
        }

     
    }
}
