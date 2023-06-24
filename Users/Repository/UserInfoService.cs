using Users.Interface;
using Users.Model;
using UsersTests;

namespace Users.Repository
{
    public class UserInfoService : IUserInfo
    {
        //private DatabaseContext _dbContext;
        //private readonly IMapper _mapper;

        //public UserInfoService(DatabaseContext dbContext, IMapper mapper)
        //{
        //    _dbContext = dbContext;
        //    _mapper = mapper;
        //}
        private MockHelper listOfUsers = new MockHelper();
        public Task<int> CreateUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public List<UserInfo> GetAllUsers()
        {
            try
            {

                return listOfUsers.GetUserInfoList();
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        public Task<IEnumerable<UserInfo>> GetUserInfoAsync()
        {
            throw new NotImplementedException();
        }

        public UserInfo GetUserInfoById(int id)
        {
            return listOfUsers.GetUserInfoList().FirstOrDefault(_ => _.Id == id);
        }

        public Task<UserInfo> GetUserInfoByIdAsunc(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }
    }
}
