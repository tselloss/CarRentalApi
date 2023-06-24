using Users.Model;

namespace Users.Interface
{
    public interface IUserInfo
    {
        Task<int> CreateUser(UserInfo userInfo);

        Task UpdateUser(UserInfo userInfo);
        Task DeleteUser(int id);

        public List<UserInfo> GetAllUsers();
        UserInfo GetUserInfoById(int id);
    }
}
