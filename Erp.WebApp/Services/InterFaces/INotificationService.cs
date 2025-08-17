using System.Collections.Generic;
using System.Threading.Tasks;
using Erp_V1.DAL;
using Erp_V1.DAL.DAL;

namespace Erp.WebApp.Services.Interfaces
{
    public interface INotificationService
    {
        Task<Notifications> CreateNotificationAsync(string title, string message, string type,
            string targetUrl = null, string relatedEntityType = null, int? relatedEntityId = null,
            string severity = "Info", int? employeeId = null);

        Task<List<Notifications>> GetNotificationsAsync(int? employeeId = null, bool includeRead = true,
            int pageSize = 50, int pageNumber = 1);

        Task<List<Notifications>> GetUnreadNotificationsAsync(int? employeeId = null);

        Task<Notifications> GetNotificationByIdAsync(int id);

        Task<bool> MarkAsReadAsync(int notificationId, int? employeeId = null);

        Task<bool> MarkAllAsReadAsync(int? employeeId = null);

        Task<bool> DeleteNotificationAsync(int notificationId);

        Task<bool> DeleteAllReadNotificationsAsync(int? employeeId = null);

        Task<NotificationSettings> GetNotificationSettingsAsync(int employeeId, string notificationType);

        Task<List<NotificationSettings>> GetAllNotificationSettingsAsync(int employeeId);

        Task<bool> UpdateNotificationSettingsAsync(int employeeId, string notificationType,
            bool isEnabled, decimal? thresholdValue = null, string additionalSettings = null);

        Task BroadcastToAllAsync(string title, string message, string type = "Info",
            string targetUrl = null, string severity = "Info");

        Task BroadcastToRoleAsync(string role, string title, string message, string type = "Info",
            string targetUrl = null, string severity = "Info");

        Task BroadcastToUserAsync(int employeeId, string title, string message, string type = "Info",
            string targetUrl = null, string severity = "Info");

        Task<int> GetUnreadCountAsync(int? employeeId = null);

        Task<Dictionary<string, int>> GetNotificationStatsAsync(int? employeeId = null);

        Task CreateSystemNotificationAsync(string title, string message, string type = "System",
            string severity = "Info", string targetUrl = null);

        Task CreateLowStockNotificationAsync(string productName, int currentStock, int threshold);

        Task CreateSalesTargetNotificationAsync(string message, decimal currentAmount, decimal targetAmount);

        Task CreatePaymentReminderNotificationAsync(string customerName, decimal amount, System.DateTime dueDate);
    }
}