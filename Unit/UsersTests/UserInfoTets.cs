using NUnit.Framework;
using Users.Model;
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
            //UserInfoService repository = new UserInfoService();

            //// Act
            //List<UserInfo> users = repository.GetAllUsers();

            //// Assert
            //Assert.IsNotNull(users);
            //Assert.IsInstanceOf<List<UserInfo>>(users);
            //Assert.IsTrue(users.Count > 0);
        }
    }
}

