using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.ViewModels.Shared;

namespace LittleBlog.ViewModels.Identity
{
    public class SignInViewModel : ViewModel
    {
        [DisplayName("Username")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        public string Password { get; set; }
    }
}
