using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using LittleBlog.DAL.Persistence;
using LittleBlog.DAL.Persistence.UnitsOfWork;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Identity;
using LittleBlog.Exceptions;
using Microsoft.AspNet.Identity;

namespace LittleBlog.BLL.Services.Implementation
{
    public class AccountService : Service<IIdentityUnitOfWork>, IAccountService
    {
        public AccountService(IIdentityUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {}
        
        public async void Create(AccountDTO accountDto)
        {
            accountDto.RoleName = accountDto.RoleName ?? "User";

            var userManager = this.UnitOfWork.UserManager;

            var user = await userManager.FindByEmailAsync(accountDto.Email);

            if (user != null)
                throw IdentityException.UserIsExists(accountDto.Email);

            var account = Mapper.Map<AccountDTO, Account>(accountDto);
            
            user = new AppUser
            {
                Email = accountDto.Email,
                UserName = accountDto.Email,
                Account = account
            };

            var userResult = userManager.Create(user, accountDto.Password);

            if (!userResult.Succeeded)
                throw IdentityException.CreateingUserFailure(accountDto.Email);

            var roleResult = UnitOfWork.UserManager.AddToRole(user.Id, accountDto.RoleName);

            if (!roleResult.Succeeded)
                throw IdentityException.AddToRoleUserFailure(accountDto.Email, accountDto.RoleName);
        }

        public AccountDTO GetUserByName(string userName)
        {
            var user = UnitOfWork.UserManager.FindByName(userName);
            return Mapper.Map<Account, AccountDTO>(user.Account);
        }
    }
}