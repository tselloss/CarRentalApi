using FluentAssertions;
using NUnit.Framework;
using Users.Repository;

namespace UsersTests
{
    public class UserInfoTets
    {
        private MockHelper _mockHelper;

        [SetUp]
        public void Setup()
        {
            _mockHelper = new MockHelper();
        }

        [Test]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            var expectedUsers = _mockHelper.GetUserInfoList();
            var userInfoService = new UserInfoService();

            // Act
            var actualUsers = userInfoService.GetAllUsers();

            // Assert
            actualUsers.Should().BeEquivalentTo(expectedUsers);
        }
    }
}

