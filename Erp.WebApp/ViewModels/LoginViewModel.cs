using System.ComponentModel.DataAnnotations;

namespace Erp.WebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Number")]
        public int UserNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}