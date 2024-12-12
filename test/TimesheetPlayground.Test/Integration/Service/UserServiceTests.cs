using System;
using Xunit;
using TimesheetPlayground.UI.BLL;
using TimesheetPlayground.UI.Models.DTO;
using TimesheetPlayground.UI.Models.Enum;
using System.Threading.Tasks;

namespace TimesheetPlayground.Test.Integration.Service
{
    public class UserServiceTests
    {
        private readonly UserService userService;
        private readonly UserDO storedUser;

        private void AssertAllFields(UserDO actual)
        {
            Assert.NotNull(actual);
            Assert.Equal(storedUser.Name, actual.Name);
            Assert.Equal(storedUser.Password, actual.Password);
            Assert.Equal(storedUser.Role, actual.Role);
        }

        public UserServiceTests()
        {
            userService = new UserService();
            storedUser = userService.InsertUser(new UserDO()
            { 
                Name = "TestName" + Guid.NewGuid().ToString(),
                Password = Guid.NewGuid().ToString(),
                Role = RoleEnum.Employee,
            });
        }

        [Fact]
        public async Task GetUserByIdAsync_ValidUserId_ValidUserDO()
        {
            // arrange, act
            var actual = await userService.GetUserByIdAsync(storedUser.Id);

            // assert
            AssertAllFields(actual);
        }

        [Fact]
        public async Task GetUserByIdAsync_InvalidUserId_ReturnNull()
        {
            // arrange, act
            var actual = await userService.GetUserByIdAsync(-1);

            // assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetUserByNamePasswordAsync_ValidUserNameAndPassword_ValidUserDO()
        {
            // arrange, act
            var actual = await userService.GetUserByNamePasswordAsync(storedUser.Name, storedUser.Password);

            // assert
            AssertAllFields(actual);
        }

        [Fact]
        public async Task GetUserByNamePasswordAsync_InvalidUserNamePasswordPair_ReturnNull()
        {
            // arrange, act
            var actual = await userService.GetUserByNamePasswordAsync("INVALID", "INVALID");

            // assert
            Assert.Null(actual);
        }
    }
}
