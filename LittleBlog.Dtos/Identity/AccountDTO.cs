using LittleBlog.Dtos.Shared;
   
   namespace LittleBlog.Dtos.Identity
   {
       public class AccountDTO : DTO
       {
           public string Email { get; set; }
           
           public string Nick { get; set; }
  
           public string Password { get; set; }
  
           public string RoleName { get; set; }
           
           // TODO: Register Account
       }
   }