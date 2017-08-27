using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LittleBlog.Entities.Shared;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LittleBlog.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public virtual Account Account { get; set; }
    }

    public class Account : Entity
    {
        public string Nick { get; set; }
        
        // TODO: Add metric fields
    }
}
