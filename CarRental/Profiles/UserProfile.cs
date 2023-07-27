using AutoMapper;
using Users.Entities;
using Users.Model;

namespace Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserInfo>().ReverseMap();
        }
    }
}
