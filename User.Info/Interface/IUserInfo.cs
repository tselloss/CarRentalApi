using Users.Entities;

namespace User.Info.Interface
{
    public interface IUserInfo
    {
        Task UpdateUserAsync(UserEntity userEntity);
        void DeleteUserAsync(UserEntity userEntity);

        Task<IEnumerable<UserEntity>> GetAllUsersAsync();
        Task<UserEntity?> GetUserInfoByIdAsync(int id);
        Task Register(UserEntity userEntity);
    }
}
