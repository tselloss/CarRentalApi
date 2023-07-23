using Users.Model;

namespace UsersTests
{
    public class MockHelper
    {
        public List<UserInfo> GetUserInfoList()
        {
            List<UserInfo> users = new List<UserInfo>()
            {
                new UserInfo
                {
                    Id = 1,
                    Name = "John Doe",
                    Email = "johndoe@example.com",
                    Password = "password123",
                    Address = "123 Main Street",
                    City = "New York",
                    PostalCode = 10001
                },
                new UserInfo
                {
                    Id = 2,
                    Name = "Jane Smith",
                    Email = "janesmith@example.com",
                    Password = "pass123",
                    Address = "456 Elm Street",
                    City = "Los Angeles",
                    PostalCode = 90001
                },
                new UserInfo
                {
                    Id = 3,
                    Name = "Alice Johnson",
                    Email = "alicejohnson@example.com",
                    Password = "alicepass",
                    Address = "789 Oak Street",
                    City = "Chicago",
                    PostalCode = 60601
                }
            };

            return users;
        }
    }
}
