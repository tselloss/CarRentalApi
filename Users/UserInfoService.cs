namespace Users
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
        public Task<int> CreateUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserInfo>> GetUserInfoAsync()
        {
            throw new NotImplementedException();
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
