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
                var DbOptions = new DbContextOptionsBuilder<ApplicationContex>().UseInMemoryDatabase("TestUserDb").Options;
                using (var contexdb = new ApplicationContex(DbOptions))
                {
                    if (!contexdb.Users.Any())
                    {
                        contexdb.AddRange(users);
                        contexdb.SaveChanges();
                    }
                }

                return new ApplicationContex(DbOptions);
            }
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
    }
}
