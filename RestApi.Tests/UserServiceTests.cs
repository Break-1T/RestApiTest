using System;
using System.Collections.Generic;
using Contex;
using Contex.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace RestApi.Tests
{
    class UserServiceTests
    {
        [Fact]
        public void Get_users_test()
        {
            // Arrange

            var options = new DbContextOptionsBuilder<ApplicationContex>().UseIn

            var dataSource = new List<User>();
            var user = new User() { Age = 18, CurrentTime = DateTime.Now, Id = 1, Name = "Taras", Surname = "Krupko" };
            var mockSet = new MockDbSet<User>(dataSource);
            var mockContex = new Mock<ApplicationContex>();
            mockContex.Setup(db => db.Set<User>()).Returns(mockSet.Object);


            //var mock = new Mock<IUserService>();
            //mock.Setup(repo => repo.GetUserAsync()).Returns();

            // Act


            // Assert
            //UserService user = new UserService(new ApplicationContex());
        }
    }
}
