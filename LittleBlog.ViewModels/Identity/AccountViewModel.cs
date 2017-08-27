using System.ComponentModel.DataAnnotations;

namespace LittleBlog.ViewModels.Identity
{
    public class AccountViewModel
    {
        public string Email { get; set; }

        public string Nick { get; set; }

        public string Password { get; set; }
    }
}