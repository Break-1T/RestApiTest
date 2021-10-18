using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contex;
using Contex.Infrastructure;
using Contex.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using WebApplication3;
using WebApplication3.Controllers;
using Xunit;
using Moq;

namespace RestApi.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<ApplicationContex> _dbContextMock;
        private readonly Mock<ILogger<UserController>> _userControllerloggerMock;
        private readonly Mock<ILogger<UserService>> _userServiceloggerMock;


        private readonly List<User> _usersList = new()
        {
            new() { Age = 19, CurrentTime = DateTime.Now, Id = 1, Name = "taras", Surname = "krupko" },
            new() { Age = 20, CurrentTime = DateTime.Now, Id = 2, Name = "ivan", Surname = "sidorov" },
            new() { Age = 21, CurrentTime = DateTime.Now, Id = 3, Name = "valera", Surname = "ovechkin" },
            new() { Age = 22, CurrentTime = DateTime.Now, Id = 4, Name = "roman", Surname = "sochin" }
        };

        private ApplicationContex _appContex
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<ApplicationContex>().UseInMemoryDatabase($"DB: {Guid.NewGuid()}").Options;
                var appContex = new ApplicationContex(dbOptions);

                appContex.Users.AddRange(_usersList);
                appContex.SaveChanges();
                return appContex;

            }
        }


        public UserControllerTests()
        {
            _dbContextMock = new Mock<ApplicationContex>();

            _userControllerloggerMock = new Mock<ILogger<UserController>>();

            _userServiceloggerMock = new Mock<ILogger<UserService>>();
        }

        [Fact]
        public async void Get_Async_Test()
        {
            //Arrange

            var user = new UserController(_userControllerloggerMock.Object,
                new UserService(_appContex,_userServiceloggerMock.Object));

            //Act

            var result = await user.GetAsync();

            //Assert

            Assert.NotEmpty(result);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(999)]
        public async void Get_Async__By_Id_Test(int id)
        {
            //Arrange

            var user = new UserController(_userControllerloggerMock.Object,
                new UserService(_appContex, _userServiceloggerMock.Object));
            //Act

            var result = await user.GetAsync(id);

            //Assert
            if (id==999)
                Assert.Null(result);
            else
                Assert.Equal(id,result.Id);
        }
    }
}
