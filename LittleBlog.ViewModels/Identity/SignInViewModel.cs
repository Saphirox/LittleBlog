using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Identity
{
    public class SignInViewModel : ViewModel
    {
        [Required]
        [DisplayName("Username")]
        public string Email { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}
