using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contex;
using Contex.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace RestApi.Tests
{
    public class UserServiceTests
    {
        private ApplicationContex contex
        {
            get
            {
                var users = new List<User>
                {
                    new () {Age = 19, CurrentTime = DateTime.Now, Id = 1, Name = "taras", Surname = "krupko"},
                    new () {Age = 20, CurrentTime = DateTime.Now, Id = 2, Name = "ivan", Surname = "sidorov"},
                    new () {Age = 21, CurrentTime = DateTime.Now, Id = 3, Name = "valera", Surname = "ovechkin"},
                    new () {Age = 22, CurrentTime = DateTime.Now, Id = 4, Name = "roman", Surname = "sochin"}
                };
                var DbOptions = new DbContextOptionsBuilder<ApplicationContex>().UseInMemoryDatabase($"TestUserDb{Guid.NewGuid()}").Options;
                using (var dbContex = new ApplicationContex(DbOptions))
                {
                    if (!dbContex.Users.Any())
                    {
                        dbContex.AddRange(users);
                        dbContex.SaveChanges();
                    }
                }

                return new ApplicationContex(DbOptions);
            }
        }

        public void Get_Users_Moq_Test()
        {
            var users = new List<User>
            {
                new () {Age = 19, CurrentTime = DateTime.Now, Id = 1, Name = "taras", Surname = "krupko"},
                new () {Age = 20, CurrentTime = DateTime.Now, Id = 2, Name = "ivan", Surname = "sidorov"},
                new () {Age = 21, CurrentTime = DateTime.Now, Id = 3, Name = "valera", Surname = "ovechkin"},
                new () {Age = 22, CurrentTime = DateTime.Now, Id = 4, Name = "roman", Surname = "sochin"}
            };
            var mockDbContext = new Mock<ApplicationContex>();
            mockDbContext.Setup(d => d.Users)
                .Returns(users);
            mockDbContext.Setup(d => d.Users)
                .ThrowsAsync(new Exception());

            var service = new UserService(mockDbContext)

                ser



            mockDbContext.Object.Users.AddRange(users);
        }

        [Fact]
        public async void Get_users_notNULL_test()
        {
            // Arrange

            UserService service = new UserService(contex);

            // Act

            var result = await service.GetUserAsync(new CancellationToken());

            // Assert

            Assert.NotNull(result);
        }
        [Fact]
        public async void Get_users_is_List_test()
        {
            // Arrange

            UserService service = new UserService(contex);

            // Act

            var result = await service.GetUserAsync(new CancellationToken());

            // Assert

            Assert.IsType<List<User>>(result);
        }
        [Fact]
        public async void Get_user_by_Id_test()
        {
            // Arrange

            UserService service = new UserService(contex);
            int id = 2;
            // Act

            var result = await service.GetUserAsync(id,new CancellationToken());

            // Assert

            Assert.Equal(2,result.Id);
        }
        [Fact]
        public async void Add_user_test()
        {
            // Arrange

            UserService service = new UserService(contex);

            // Act

            var result = await service.AddUserAsync(new CancellationToken());

            // Assert

            Assert.Equal(true,result);
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

            UserService service = new UserService(contex);

            // Act

            var result = await service.DeleteUserAsync(Id, new CancellationToken());

            // Assert
            if (Id==100)
                Assert.Equal(false, result);
            else
                Assert.Equal(true, result);
        }
    }
}
