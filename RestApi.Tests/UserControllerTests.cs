using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Context;
using Context.Infrastructure;
using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Api;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Moq;

namespace RestApi.Tests
{
    public class UserControllerTests
    {
        public UserControllerTests()
        {
            _userControllerMock = new Mock<UserController>();

            _userServiceMock = new Mock<UserService>();

            _userControllerloggerMock = new Mock<ILogger<UserController>>();

            _userServiceloggerMock = new Mock<ILogger<UserService>>();

        }
        
        private readonly Mock<ILogger<UserController>> _userControllerloggerMock;
        private readonly Mock<ILogger<UserService>> _userServiceloggerMock;
        private readonly Mock<UserService> _userServiceMock;
        private readonly Mock<UserController> _userControllerMock;

        private readonly IEnumerable<User> _usersList = new List<User>()
        {
            new() { Age = 19, CurrentTime = DateTime.Now, Id = 1, Name = "taras", Surname = "krupko" },
            new() { Age = 20, CurrentTime = DateTime.Now, Id = 2, Name = "ivan", Surname = "sidorov" },
            new() { Age = 21, CurrentTime = DateTime.Now, Id = 3, Name = "valera", Surname = "ovechkin" },
            new() { Age = 22, CurrentTime = DateTime.Now, Id = 4, Name = "roman", Surname = "sochin" }
        };

        private RestApiContext AppContext
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<RestApiContext>().UseInMemoryDatabase($"DB: {Guid.NewGuid()}").Options;
                var appContex = new RestApiContext(dbOptions);

                appContex.Users.AddRange(_usersList);
                appContex.SaveChanges();
                return appContex;

            }
        }

        [Fact]
        public async void GetAsync_NotEmpty_Test()
        {
            //Arrange
            
            var userController = new UserController(_userControllerloggerMock.Object, _userServiceMock.Object);
            _userServiceMock.Setup(service => service.GetUserAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_usersList));

            //Act

            var result = await userController.GetUsersAsync();

            //Assert

            //Assert.Equal(result.ExecuteResultAsync().Status,HttpStatusCode.OK);
            //Assert.Equal(_usersList.Count(), result.Count());

            _userControllerloggerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void GetAsync_NotNull_Test()
        {
            //Arrange

            var userController = new UserController(_userControllerloggerMock.Object, _userServiceMock.Object);
            _userServiceMock.Setup(service => service.GetUserAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(_usersList));
            //Act

            var result = await userController.GetUsersAsync();

            //Assert

            Assert.NotNull(result);
            //Assert.Equal(_usersList.Count(),result.Count());

            _userControllerloggerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void GetAsync_Exception_Test()
        {
            //Arrange

            var exception = new Exception("GetAsync_Exception_Test");
            _userServiceMock.Setup(userService => userService.GetUserAsync(It.IsAny<CancellationToken>())).Throws(exception);
            var userController = new UserController(_userControllerloggerMock.Object,_userServiceMock.Object);

            //Act

            var result = await userController.GetUsersAsync();

            //Assert

            Assert.Null(result);

            _userControllerloggerMock.Verify(x=>x.Log(LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                exception,
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),Times.Once);
            _userServiceMock.Verify(x=>x.GetUserAsync(It.IsAny<CancellationToken>()),Times.Once);


            _userControllerloggerMock.VerifyNoOtherCalls();
            _userServiceMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(999)]
        public async void GetAsync__By_Id_Test(int id)
        {
            //Arrange

            var user = new UserController(_userControllerloggerMock.Object,
                new UserService(AppContext, _userServiceloggerMock.Object));
            //Act

            var result = await user.GetUserAsync(id);

            //Assert
            if (id==999)
                Assert.Null(result);
            //else
                //Assert.Equal(id,result.Id);
        }
    }
}
