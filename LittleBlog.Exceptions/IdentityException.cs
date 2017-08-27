using System;

namespace LittleBlog.Exceptions
{
    public class IdentityException : Exception
    {
        public IdentityException(string message) : base(message)
        {}

        public static IdentityException UserIsExists(string userName)
        {
            return new IdentityException($"User with name {userName} is exits");
        }

        public static IdentityException UserNotFound(string userName)
        {
            return new IdentityException($"User with name {userName} is not found");
        }

        public static IdentityException CreateingUserFailure(string userName)
        {  
            return new IdentityException($"User with name {userName} cant be create");
        }

        public static IdentityException AddToRoleUserFailure(string userName, string roleName)
        {
            return new IdentityException($"User with name {userName} can't add role {roleName}");

        }
    }
}