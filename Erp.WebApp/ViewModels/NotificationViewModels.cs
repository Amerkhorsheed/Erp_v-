using System.ComponentModel.DataAnnotations;

namespace Erp.WebApp.ViewModels
{
    public class CreateNotificationViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Display(Name = "Severity")]
        public string Severity { get; set; }

        [Display(Name = "Target URL")]
        public string TargetUrl { get; set; }

        [Required]
        [Display(Name = "Target Type")]
        public string TargetType { get; set; } // All, Role, User

        [Display(Name = "Target Role")]
        public string TargetRole { get; set; }

        [Display(Name = "Target User ID")]
        public int? TargetUserId { get; set; }
    }

    public class NotificationSettingViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Notification Type")]
        public string NotificationType { get; set; }

        [Display(Name = "Enabled")]
        public bool IsEnabled { get; set; }

        [Display(Name = "Threshold Value")]
        public decimal? ThresholdValue { get; set; }

        [Display(Name = "Additional Settings")]
        public string AdditionalSettings { get; set; }
    }
}