using Erp.WebApp.Services.Interfaces;
using Erp_V1.DAL;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Erp.WebApp.Hubs;
using Erp_V1.DAL.DAL;
using System.Globalization;

namespace Erp.WebApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly erp_v2Entities _context;
        private readonly IHubContext _hubContext;

        public NotificationService(erp_v2Entities context)
        {
            _context = context;
            _hubContext = GlobalHost.ConnectionManager.GetHubContext<NotificationHub>();
        }

        public async Task<Notifications> CreateNotificationAsync(string title, string message, string type, 
            string targetUrl = null, string relatedEntityType = null, int? relatedEntityId = null, 
            string severity = "Info", int? employeeId = null)
        {
            var notification = new Notifications
            {
                Title = title,
                Message = message,
                Type = type,
                Timestamp = DateTime.Now,
                IsRead = false,
                TargetUrl = targetUrl,
                RelatedEntityType = relatedEntityType,
                RelatedEntityId = relatedEntityId,
                Severity = severity,
                IsDeleted = false,
                EmployeeId = employeeId
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Broadcast notification via SignalR
            await BroadcastNotificationAsync(notification);

            return notification;
        }

        public async Task<List<Notifications>> GetNotificationsAsync(int? employeeId = null, bool includeRead = true, 
            int pageSize = 50, int pageNumber = 1)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            if (!includeRead)
            {
                query = query.Where(n => !n.IsRead);
            }

            return await query
                .OrderByDescending(n => n.Timestamp)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<List<Notifications>> GetUnreadNotificationsAsync(int? employeeId = null)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted && !n.IsRead);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            return await query
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();
        }

        public async Task<Notifications> GetNotificationByIdAsync(int id)
        {
            return await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == id && !n.IsDeleted);
        }

        public async Task<bool> MarkAsReadAsync(int notificationId, int? employeeId = null)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId && !n.IsDeleted);

            if (notification == null)
                return false;

            // Check if user has permission to mark this notification as read
            if (employeeId.HasValue && notification.EmployeeId.HasValue && 
                notification.EmployeeId != employeeId)
                return false;

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MarkAllAsReadAsync(int? employeeId = null)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted && !n.IsRead);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            var notifications = await query.ToListAsync();
            
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId);

            if (notification == null)
                return false;

            notification.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAllReadNotificationsAsync(int? employeeId = null)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted && n.IsRead);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            var notifications = await query.ToListAsync();
            
            foreach (var notification in notifications)
            {
                notification.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<NotificationSettings> GetNotificationSettingsAsync(int employeeId, string notificationType)
        {
            return await _context.NotificationSettings
                .FirstOrDefaultAsync(ns => ns.EmployeeId == employeeId && 
                                         ns.NotificationType == notificationType && 
                                         !ns.IsDeleted);
        }

        public async Task<List<NotificationSettings>> GetAllNotificationSettingsAsync(int employeeId)
        {
            return await _context.NotificationSettings
                .Where(ns => ns.EmployeeId == employeeId && !ns.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> UpdateNotificationSettingsAsync(int employeeId, string notificationType, 
            bool isEnabled, decimal? thresholdValue = null, string additionalSettings = null)
        {
            var setting = await _context.NotificationSettings
                .FirstOrDefaultAsync(ns => ns.EmployeeId == employeeId && 
                                         ns.NotificationType == notificationType && 
                                         !ns.IsDeleted);

            if (setting == null)
            {
                setting = new NotificationSettings
                {
                    EmployeeId = employeeId,
                    NotificationType = notificationType,
                    IsEnabled = isEnabled,
                    ThresholdValue = thresholdValue?.ToString(CultureInfo.InvariantCulture),
                    AdditionalSettings = additionalSettings,
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    IsDeleted = false
                };
                _context.NotificationSettings.Add(setting);
            }
            else
            {
                setting.IsEnabled = isEnabled;
                setting.ThresholdValue = thresholdValue?.ToString(CultureInfo.InvariantCulture);
                setting.AdditionalSettings = additionalSettings;
                setting.ModifiedDate = DateTime.Now;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task BroadcastToAllAsync(string title, string message, string type = "Info", 
            string targetUrl = null, string severity = "Info")
        {
            var notification = await CreateNotificationAsync(title, message, type, targetUrl, 
                null, null, severity, null);
            
            _hubContext.Clients.All.receiveNotification(new
            {
                id = notification.Id,
                title = notification.Title,
                message = notification.Message,
                type = notification.Type,
                severity = notification.Severity,
                timestamp = notification.Timestamp,
                targetUrl = notification.TargetUrl
            });
        }

        public async Task BroadcastToRoleAsync(string role, string title, string message, string type = "Info", 
            string targetUrl = null, string severity = "Info")
        {
            var notification = await CreateNotificationAsync(title, message, type, targetUrl, 
                null, null, severity, null);
            
            _hubContext.Clients.Group(role).receiveNotification(new
            {
                id = notification.Id,
                title = notification.Title,
                message = notification.Message,
                type = notification.Type,
                severity = notification.Severity,
                timestamp = notification.Timestamp,
                targetUrl = notification.TargetUrl
            });
        }

        public async Task BroadcastToUserAsync(int employeeId, string title, string message, string type = "Info", 
            string targetUrl = null, string severity = "Info")
        {
            var notification = await CreateNotificationAsync(title, message, type, targetUrl, 
                null, null, severity, employeeId);
            
            _hubContext.Clients.Group($"User_{employeeId}").receiveNotification(new
            {
                id = notification.Id,
                title = notification.Title,
                message = notification.Message,
                type = notification.Type,
                severity = notification.Severity,
                timestamp = notification.Timestamp,
                targetUrl = notification.TargetUrl
            });
        }

        public async Task<int> GetUnreadCountAsync(int? employeeId = null)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted && !n.IsRead);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            return await query.CountAsync();
        }

        public async Task<Dictionary<string, int>> GetNotificationStatsAsync(int? employeeId = null)
        {
            var query = _context.Notifications.Where(n => !n.IsDeleted);

            if (employeeId.HasValue)
            {
                query = query.Where(n => n.EmployeeId == employeeId || n.EmployeeId == null);
            }

            var stats = await query
                .GroupBy(n => n.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Type, x => x.Count);

            return stats;
        }

        public async Task CreateSystemNotificationAsync(string title, string message, string type = "System", 
            string severity = "Info", string targetUrl = null)
        {
            await BroadcastToAllAsync(title, message, type, targetUrl, severity);
        }

        public async Task CreateLowStockNotificationAsync(string productName, int currentStock, int threshold)
        {
            var title = "Low Stock Alert";
            var message = $"Product '{productName}' is running low. Current stock: {currentStock}, Threshold: {threshold}";
            await BroadcastToRoleAsync("Admin", title, message, "LowStock", "/Admin/Products", "Warning");
        }

        public async Task CreateSalesTargetNotificationAsync(string message, decimal currentAmount, decimal targetAmount)
        {
            var title = "Sales Target Update";
            var severity = currentAmount >= targetAmount ? "Success" : "Warning";
            await BroadcastToRoleAsync("Admin", title, message, "SalesTarget", "/Admin/Sales", severity);
        }

        public async Task CreatePaymentReminderNotificationAsync(string customerName, decimal amount, DateTime dueDate)
        {
            var title = "Payment Reminder";
            var message = $"Payment of ${amount:F2} from {customerName} is due on {dueDate:MMM dd, yyyy}";
            var severity = dueDate <= DateTime.Now ? "Error" : "Warning";
            await BroadcastToRoleAsync("Admin", title, message, "PaymentReminder", "/Admin/Invoices", severity);
        }

        private async Task BroadcastNotificationAsync(Notifications notification)
        {
            var notificationData = new
            {
                id = notification.Id,
                title = notification.Title,
                message = notification.Message,
                type = notification.Type,
                severity = notification.Severity,
                timestamp = notification.Timestamp,
                targetUrl = notification.TargetUrl
            };

            if (notification.EmployeeId.HasValue)
            {
                // Send to specific user
                _hubContext.Clients.Group($"User_{notification.EmployeeId}").receiveNotification(notificationData);
            }
            else
            {
                // Send to all users
                _hubContext.Clients.All.receiveNotification(notificationData);
            }
        }
    }
}