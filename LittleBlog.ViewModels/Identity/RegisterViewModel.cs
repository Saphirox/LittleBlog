using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Identity
{
    public class RegisterViewModel : ViewModel
    {
        [DisplayName("Username")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Enter correct name of email")]
        public string Email { get; set; }

        [DisplayName("Password")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Password must have less then 10 and more than 5 characters")]
        public string Password { get; set; }

        [DisplayName("Nickname")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Nickname must have less then 10 and more than 5 characters")]
        public string Nick { get; set; }
        
    }
}