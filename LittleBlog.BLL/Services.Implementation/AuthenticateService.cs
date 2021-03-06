﻿using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Identity;
using LittleBlog.Exceptions;
using Microsoft.AspNet.Identity;

namespace LittleBlog.BLL.Services.Implementation
{
    public class AuthenticationService : Service<IIdentityUnitOfWork>, IAuthenticationService
    {
        public AuthenticationService(IIdentityUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ClaimsIdentity> Authenticate(AccountDTO accountDto)
        {
            ClaimsIdentity identity = null;            
            
            var user = await UnitOfWork.UserManager.FindAsync(accountDto.Email, accountDto.Password);

            if (user == null)
                throw IdentityException.UserNotFound(accountDto.Email);
            
            identity = await UnitOfWork.UserManager.CreateIdentityAsync(user,
                DefaultAuthenticationTypes.ApplicationCookie);

            return identity;
        }
    }
}