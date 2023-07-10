using NUnit.Framework;
using Users.Model;
using Users.Repository;

namespace UsersTests
{
    public class UserInfoTets
    {
        private MockHelper _mockHelper;
        private string connectionString = "Server=127.0.0.1;Database=CarManagementProject;Username=postgres;Password=admin;Persist Security Info=True";

        [SetUp]
        public void Setup()
        {
            _mockHelper = new MockHelper();
        }

        [Test]
        public void GetAllUsers_ReturnsListOfUsers()
        {
            // Arrange
            UserInfoService repository = new UserInfoService(connectionString);

            // Act
            List<UserInfo> users = repository.GetAllUsers();

            // Assert
            Assert.IsNotNull(users);
            Assert.IsInstanceOf<List<UserInfo>>(users);
            Assert.IsTrue(users.Count > 0);
        }
    }
}

