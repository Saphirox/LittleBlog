using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using LittleBlog.BLL.Services;
using LittleBlog.BLL.Services.Implementation;
using LittleBlog.DAL.Identity;
using LittleBlog.DAL.Repositories;
using LittleBlog.Dtos.Identity;
using LittleBlog.Entities.Identity;
using LittleBlog.Mapper;
using Microsoft.AspNet.Identity;
using Moq;
using NUnit.Framework;

namespace LittleBlog.Tests
{
    [TestFixture]
    public class AccountServiceTest
    {
        private Mock<IIdentityUnitOfWork> _mockIdentityUnitOfWork;
        
        private Mock<AppUserManager> _mockAppUserManager;

        private Mock<IUserStore<AppUser>> _mockUserStore; 
        
        private IAccountService _accountService;
        private IMapper _mapper;

        private IList<AppUser> userContext;


        [SetUp]
        public void SetUp()
        {
            _mapper = MapperBuilder.BuildMapper();
            _mockUserStore = new Mock<IUserStore<AppUser>>();
            _mockAppUserManager = new Mock<AppUserManager>(_mockUserStore.Object);

            _mockAppUserManager.Setup(s => s.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .Returns(Task.FromResult(IdentityResult.Success))
                .Callback((AppUser a, string password) =>
                {
                    userContext.Add(a);
                });

            _mockIdentityUnitOfWork = new Mock<IIdentityUnitOfWork>();

            _mockIdentityUnitOfWork.Setup(s => s.UserManager)
                .Returns(_mockAppUserManager.Object);

            userContext = new List<AppUser>();

        }

        [Test]
        public void CreateUser_Test()
        {

            _accountService = new AccountService(_mockIdentityUnitOfWork.Object, _mapper);
            
            var fakeUserDto = new AccountDTO { Password = "12356" };

            _accountService.Create(fakeUserDto);
            
            Assert.AreEqual(1, userContext.Count);
        }
    }
}