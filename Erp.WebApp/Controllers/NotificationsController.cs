using Erp.WebApp.Controllers;
using Erp.WebApp.Services.Interfaces;
using Erp.WebApp.Services;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using Erp_V1.DAL.DAL;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Globalization;
using Erp.WebApp.ViewModels;
using System.Configuration;

namespace Erp.WebApp.Controllers
{
    [CustomAuthorize]
    public class NotificationsController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly IAuthService _authService;

        public NotificationsController(INotificationService notificationService, IAuthService authService)
        {
            _notificationService = notificationService;
            _authService = authService;
        }

        // GET: Notifications
        public async Task<ActionResult> Index(int page = 1, bool includeRead = true, string type = null)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var notifications = await _notificationService.GetNotificationsAsync(
                    currentUserId, includeRead, 20, page);

                if (!string.IsNullOrEmpty(type))
                {
                    notifications = notifications.Where(n => n.Type == type).ToList();
                }

                ViewBag.CurrentPage = page;
                ViewBag.IncludeRead = includeRead;
                ViewBag.Type = type;
                ViewBag.UnreadCount = await _notificationService.GetUnreadCountAsync(currentUserId);
                ViewBag.Stats = await _notificationService.GetNotificationStatsAsync(currentUserId);
                ViewBag.IsAdmin = _authService.UserRole == "Admin";

                return View(notifications);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading notifications: " + ex.Message;
                return View(new List<Notifications>());
            }
        }

        // GET: Notifications/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var notification = await _notificationService.GetNotificationByIdAsync(id);
                if (notification == null)
                {
                    return HttpNotFound();
                }

                // Mark as read when viewing details
                var currentUserId = GetCurrentUserId();
                await _notificationService.MarkAsReadAsync(id, currentUserId);

                return View(notification);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading notification: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // GET: Notifications/Create (Admin only)
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.NotificationTypes = GetNotificationTypes();
            ViewBag.SeverityLevels = GetSeverityLevels();
            ViewBag.TargetRoles = GetTargetRoles();
            return View(new CreateNotificationViewModel());
        }

        // POST: Notifications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public async Task<ActionResult> Create(CreateNotificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    switch (model.TargetType)
                    {
                        case "All":
                            await _notificationService.BroadcastToAllAsync(
                                model.Title, model.Message, model.Type, 
                                model.TargetUrl, model.Severity);
                            break;
                        case "Role":
                            await _notificationService.BroadcastToRoleAsync(
                                model.TargetRole, model.Title, model.Message, 
                                model.Type, model.TargetUrl, model.Severity);
                            break;
                        case "User":
                            if (model.TargetUserId.HasValue)
                            {
                                await _notificationService.BroadcastToUserAsync(
                                    model.TargetUserId.Value, model.Title, model.Message, 
                                    model.Type, model.TargetUrl, model.Severity);
                            }
                            break;
                    }

                    TempData["Success"] = "Notification created and sent successfully.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error creating notification: " + ex.Message);
                }
            }

            ViewBag.NotificationTypes = GetNotificationTypes();
            ViewBag.SeverityLevels = GetSeverityLevels();
            ViewBag.TargetRoles = GetTargetRoles();
            return View(model);
        }

        // POST: Notifications/MarkAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> MarkAsRead(int id)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var success = await _notificationService.MarkAsReadAsync(id, currentUserId);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // POST: Notifications/MarkAllAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> MarkAllAsRead()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var success = await _notificationService.MarkAllAsReadAsync(currentUserId);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // POST: Notifications/Delete (Admin only)
        [HttpPost]
        [ValidateAntiForgeryToken]
        [CustomAuthorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var success = await _notificationService.DeleteNotificationAsync(id);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // POST: Notifications/DeleteAllRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> DeleteAllRead()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var success = await _notificationService.DeleteAllReadNotificationsAsync(currentUserId);
                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        // GET: Notifications/GetUnreadCount
        public async Task<JsonResult> GetUnreadCount()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var count = await _notificationService.GetUnreadCountAsync(currentUserId);
                return Json(new { count = count }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { count = 0, error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Notifications/GetLatest
        public async Task<JsonResult> GetLatest(int count = 5)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                var notifications = await _notificationService.GetNotificationsAsync(
                    currentUserId, false, count, 1);

                var result = notifications.Select(n => new
                {
                    id = n.Id,
                    title = n.Title,
                    message = n.Message,
                    type = n.Type,
                    severity = n.Severity,
                    timestamp = n.Timestamp.ToString("MMM dd, yyyy HH:mm"),
                    targetUrl = n.TargetUrl,
                    isRead = n.IsRead
                }).ToList();
                
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { error = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Notifications/Settings
        public async Task<ActionResult> Settings()
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (!currentUserId.HasValue)
                {
                    return RedirectToAction("Login", "Home");
                }

                var settings = await _notificationService.GetAllNotificationSettingsAsync(currentUserId.Value);
                var model = settings.Select(s => new NotificationSettingViewModel
                {
                    Id = s.Id,
                    NotificationType = s.NotificationType,
                    IsEnabled = s.IsEnabled,
                    ThresholdValue = string.IsNullOrWhiteSpace(s.ThresholdValue)
                        ? (decimal?)null
                        : (decimal.TryParse(s.ThresholdValue, NumberStyles.Any, CultureInfo.InvariantCulture, out var d) ? (decimal?)d : null),
                    AdditionalSettings = s.AdditionalSettings
                }).ToList();
                ViewBag.NotificationTypes = GetNotificationTypes();
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error loading settings: " + ex.Message;
                return View(new List<NotificationSettingViewModel>());
            }
        }

        // POST: Notifications/UpdateSettings
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateSettings(List<NotificationSettingViewModel> settings)
        {
            try
            {
                var currentUserId = GetCurrentUserId();
                if (!currentUserId.HasValue)
                {
                    return RedirectToAction("Login", "Home");
                }

                foreach (var setting in settings)
                {
                    await _notificationService.UpdateNotificationSettingsAsync(
                        currentUserId.Value, setting.NotificationType, setting.IsEnabled,
                        setting.ThresholdValue, setting.AdditionalSettings);
                }

                TempData["Success"] = "Notification settings updated successfully.";
                return RedirectToAction("Settings");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error updating settings: " + ex.Message;
                return RedirectToAction("Settings");
            }
        }

        // Helper methods
        private int? GetCurrentUserId()
        {
            try
            {
                // Prefer token from session via AuthService
                var token = _authService?.Token;

                // Fallback to Authorization header
                if (string.IsNullOrEmpty(token))
                {
                    var authHeader = Request?.Headers["Authorization"];
                    if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                    {
                        token = authHeader.Substring("Bearer ".Length).Trim();
                    }
                }

                // Fallback to access_token in query string (used by SignalR)
                if (string.IsNullOrEmpty(token))
                {
                    token = Request?.QueryString["access_token"];
                }

                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }

                var tokenHandler = new JwtSecurityTokenHandler();

                var secret = ConfigurationManager.AppSettings["JWT:SecretKey"];
                var issuer = ConfigurationManager.AppSettings["JWT:Issuer"];
                var audience = ConfigurationManager.AppSettings["JWT:Audience"];
                if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                {
                    return null;
                }
                var key = Encoding.UTF8.GetBytes(secret);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var sub = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                if (int.TryParse(sub, out var userId))
                {
                    return userId;
                }
            }
            catch
            {
                // ignore and return null
            }

            return null;
        }

        private List<SelectListItem> GetNotificationTypes()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Info", Text = "Information" },
                new SelectListItem { Value = "Warning", Text = "Warning" },
                new SelectListItem { Value = "Error", Text = "Error" },
                new SelectListItem { Value = "Success", Text = "Success" },
                new SelectListItem { Value = "System", Text = "System" },
                new SelectListItem { Value = "LowStock", Text = "Low Stock" },
                new SelectListItem { Value = "SalesTarget", Text = "Sales Target" },
                new SelectListItem { Value = "PaymentReminder", Text = "Payment Reminder" }
            };
        }

        private List<SelectListItem> GetSeverityLevels()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Info", Text = "Info" },
                new SelectListItem { Value = "Warning", Text = "Warning" },
                new SelectListItem { Value = "Error", Text = "Error" },
                new SelectListItem { Value = "Success", Text = "Success" }
            };
        }

        private List<SelectListItem> GetTargetRoles()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Seller", Text = "Seller" }
            };
        }
    }

    // Remove the nested View Models at the end of the file
}