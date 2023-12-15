using TableDependency.SqlClient.Base.EventArgs;
using UI_Layer.Data.AppData;

namespace UI_Layer.NotificationTableDependency
{
    public interface INotificationTableDependency
    {
        void NotificationsTableDependency(string connectionString);
    }
}
