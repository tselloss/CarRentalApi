namespace Users
{
    public interface IUserInfo
    {
        Task<IEnumerable<UserInfo>> GetUserInfoAsync();
        Task<UserInfo> GetUserInfoByIdAsunc(int id);
        Task<int> CreateUser(UserInfo userInfo);

        Task UpdateUser(UserInfo userInfo);
        Task DeleteUser(int id);
    }
}
