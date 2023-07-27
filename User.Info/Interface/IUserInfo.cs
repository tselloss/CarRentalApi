using Users.Entities;

namespace User.Info.Interface
{
    public interface IUserInfo
    {
        Task CreateUser(UserEntity userEntity);

        Task UpdateUserAsync(UserEntity userEntity);
        void DeleteUserAsync(int id, UserEntity userEntity);

        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserInfoByIdAsync(int id);

    }
}
