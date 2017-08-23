using System.ComponentModel;
using System.Data;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Identity
{
    public class RegisterViewModel : ViewModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}