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

        [Fact]
        public async void Get_Async_Test()
        {
            //Arrange

            UserController user = new UserController(new Logger<UserController>(new NullLoggerFactory()),
                new UserService(contex,new Logger<UserService>(new LoggerFactory())));

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

            UserController user = new UserController(new Logger<UserController>(new NullLoggerFactory()), new UserService(contex));

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
