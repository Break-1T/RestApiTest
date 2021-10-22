using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Context;
using Context.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using Moq;
using Moq.Language;

namespace RestApi.Tests
{
    public class UserServiceTests
    {
        public UserServiceTests()
        {
            _dbContextMock = new Mock<RestApiContext>(new DbContextOptions<RestApiContext>());

            _loggerMock = new Mock<ILogger<UserService>>();
        }

        private readonly Mock<RestApiContext> _dbContextMock;
        private readonly Mock<ILogger<UserService>> _loggerMock;

        private readonly List<User> _usersList = new ()
        {
            new() {Age = 19, CurrentTime = DateTime.Now, Id = 1, Name = "taras", Surname = "krupko"},
            new() {Age = 20, CurrentTime = DateTime.Now, Id = 2, Name = "ivan", Surname = "sidorov"},
            new() {Age = 21, CurrentTime = DateTime.Now, Id = 3, Name = "valera", Surname = "ovechkin"},
            new() {Age = 22, CurrentTime = DateTime.Now, Id = 4, Name = "roman", Surname = "sochin"}
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
        public async void Get_users_test()
        {
            // Arrange

            UserService service = new UserService(AppContext, _loggerMock.Object);

            // Act

            var result = await service.GetUserAsync(new CancellationToken());

            // Assert

            Assert.NotNull(result);
            Assert.Equal(4, result.Count());
            _loggerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void Get_users_Exception_test()
        {
            // Arrange
            
            var excep = new Exception("Get_users_Exception_test");
            //var intDbSet = new Mock<InternalDbSet<User>>();
            _dbContextMock.Setup(db => db.Users)
                .Throws(excep);
            //_dbContextMock.Setup(db => db.Users).Returns(intDbSet.Object);
            var service = new UserService(_dbContextMock.Object, _loggerMock.Object);
            

            // Act

            var result = await service.GetUserAsync(new CancellationToken());

            // Assert

            Assert.Null(result);
            
            _dbContextMock.Verify(mockDbContext => mockDbContext.Users, Times.Once);
            _loggerMock.Verify(x=>x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.IsAny<It.IsAnyType>(),
                excep,
                (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);

            //_dbContextMock.VerifyNoOtherCalls();
            _loggerMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async void Get_user_by_Id_test()
        {
            // Arrange

            UserService service = new UserService(AppContext, _loggerMock.Object);
            int id = 2;
            // Act

            var result = await service.GetUserAsync(id, new CancellationToken());

            // Assert

            Assert.NotNull(result);
            Assert.Equal(2, result.Id);

            _loggerMock.VerifyNoOtherCalls();
        }
        [Fact]
        public async void Get_user_by_Id_Exception_test()
        {
            // Arrange
            var excep = new Exception("Get_user_by_Id_Exception_test");
            _dbContextMock.Setup(db => db.Users)
                .Throws(excep);
            UserService service = new UserService(_dbContextMock.Object, _loggerMock.Object);
            int id = 2;
            // Act

            var result = await service.GetUserAsync(id, new CancellationToken());

            // Assert

            Assert.Null(result);

            _dbContextMock.Reset();
            _loggerMock.Reset();

            _dbContextMock.VerifyNoOtherCalls();
            _loggerMock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public async void Add_user_test()
        {
            // Arrange

            UserService service = new UserService(AppContext, _loggerMock.Object);

            // Act

            //var result = await service.AddUserAsync(new CancellationToken());

            // Assert

            //Assert.Equal(true, result);
        }
        [Fact]
        public async void Add_user_Exception_test()
        {
            // Arrange
            var excep = new Exception("Get_user_by_Id_Exception_test");
            _dbContextMock.Setup(db => db.Users)
                .Throws(excep);
            UserService service = new UserService(_dbContextMock.Object, _loggerMock.Object);

            // Act

            //var result = await service.AddUserAsync(new CancellationToken());

            // Assert

            //Assert.Equal(false, result);

            _dbContextMock.Reset();
            _loggerMock.Reset();

            _dbContextMock.VerifyNoOtherCalls();
            _loggerMock.VerifyNoOtherCalls();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public async void Delete_user_test(int Id)
        {
            // Arrange

            UserService service = new UserService(AppContext, _loggerMock.Object);

            // Act

            var result = await service.DeleteUserAsync(Id, new CancellationToken());

            // Assert
            if (Id == 100)
                Assert.Equal(false, result);
            else
                Assert.Equal(true, result);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(100)]
        public async void Delete_user_Exception_test(int Id)
        {
            // Arrange
            var excep = new Exception("Get_user_by_Id_Exception_test");
            _dbContextMock.Setup(db => db.Users)
                .Throws(excep);
            UserService service = new UserService(_dbContextMock.Object, _loggerMock.Object);

            // Act

            var result = await service.DeleteUserAsync(Id, new CancellationToken());

            // Assert
                Assert.Equal(false, result);

                _dbContextMock.Reset();
                _loggerMock.Reset();

                _dbContextMock.VerifyNoOtherCalls();
                _loggerMock.VerifyNoOtherCalls();
        }
    }
}
