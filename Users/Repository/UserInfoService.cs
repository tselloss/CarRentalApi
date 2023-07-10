using Npgsql;
using Users.Interface;
using Users.Model;
using UsersTests;

namespace Users.Repository
{
    public class UserInfoService : IUserInfo
    {
        //private DatabaseContext _dbContext;
        //private readonly IMapper _mapper;
        private string _connectionString;
        public UserInfoService(string connectionstring)
        {
            _connectionString = connectionstring;
        }
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
                List<UserInfo> userInfos = new List<UserInfo>();

                using (NpgsqlConnection npgsqlConnection = new NpgsqlConnection("Server=localhost;Database=CarManagmentProject;Username=postgres;Password=admin;"))
                {
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand())
                    {
                        npgsqlCommand.CommandText = "SELECT * FROM usersinfo";
                        npgsqlCommand.Connection = npgsqlConnection;

                        if (npgsqlConnection.State != System.Data.ConnectionState.Open)
                            npgsqlConnection.Open();

                        using (NpgsqlDataReader pgSqlReader = npgsqlCommand.ExecuteReader())
                        {
                            while (pgSqlReader.Read())
                            {
                                UserInfo userInfo = new UserInfo();
                                userInfo.Id = int.Parse(pgSqlReader["_id"].ToString());
                                userInfo.Name = pgSqlReader["_name"].ToString();
                                userInfo.Email = pgSqlReader["_email"].ToString();
                                userInfo.Password = pgSqlReader["_password"].ToString();
                                userInfo.Address = pgSqlReader["_address"].ToString();
                                userInfo.City = pgSqlReader["_city"].ToString();
                                userInfo.PostalCode = pgSqlReader["_postalcode"].ToString();
                                userInfos.Add(userInfo);
                            }
                        }
                    }
                }

                return userInfos;
            }
            catch
            {
                throw new Exception();
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
