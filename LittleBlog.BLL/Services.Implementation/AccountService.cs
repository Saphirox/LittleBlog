using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LittleBlog.BLL.Infrastructure;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Identity;
using LittleBlog.Exceptions;
using Microsoft.AspNet.Identity;

namespace LittleBlog.BLL.Services.Implementation
{
    public class AccountService : IAccountService
    {
        protected IIdentityUnitOfWork UnitOfWork;
        protected IMapper Mapper;

        public AccountService(
            IIdentityUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            this.Mapper = mapper;
        }
        
        public async void Create(AccountDTO accountDto)
        {
            accountDto.RoleName = accountDto.RoleName ?? "User";

            var userManager = this.UnitOfWork.UserManager;

            var user = await userManager.FindByEmailAsync(accountDto.Email);

            if (user == null)
            {
                var account = AutoMapper.Mapper.Map<AccountDTO, Account>(accountDto);
                
                user = new AppUser
                {
                    Email = accountDto.Email,
                    UserName = accountDto.Email,
                    Account = account
                };

                var result = userManager.Create(user, accountDto.Password);

                if (result.Succeeded)
                {
                    var resultRole = UnitOfWork.UserManager.AddToRole(user.Id, accountDto.RoleName);

                    if (resultRole.Succeeded)
                    {
                        return;
                    }
                    else
                    {
                        throw IdentityException.AddToRoleUserFailure(accountDto.Email, accountDto.RoleName);
                    }
                }
                else
                {
                    throw IdentityException.CreateingUserFailure(accountDto.Email);
                }
            }
            else
            {
                throw IdentityException.UserIsExists(accountDto.Email);
            }
        }

        public async Task<ClaimsIdentity> Authenticate(AccountDTO account)
        {
            ClaimsIdentity identity = null;            
            
            var user = await UnitOfWork.UserManager.FindAsync(account.Email, account.Password);

            if (user != null)
            {
                identity = await UnitOfWork.UserManager.CreateIdentityAsync(user,
                    DefaultAuthenticationTypes.ApplicationCookie);

                return identity;
            }
            else
            {
                throw IdentityException.UserNotFound(account.Email);
            }
        }

        public async Task SetInitialUser(AccountDTO account, List<string> roles)
        {
            foreach (var roleName in roles)
            {
                AppRole role = await UnitOfWork.RoleManager.FindByNameAsync(roleName);
                
                if (role == null)
                {
                    role = new AppRole { Name = roleName };
                    await UnitOfWork.RoleManager.CreateAsync(role);
                }
            }

            Create(account);
        }

        public AccountDTO GetUserAccountById(Guid id)
        {
            var user = UnitOfWork.UserManager.FindById(id.ToString("D"));
            return AutoMapper.Mapper.Map<Account, AccountDTO>(user.Account);
        }

        public AccountDTO GetUserByName(string userName)
        {
            var user = UnitOfWork.UserManager.FindByName(userName);
            return AutoMapper.Mapper.Map<Account, AccountDTO>(user.Account);
        }
    }

}