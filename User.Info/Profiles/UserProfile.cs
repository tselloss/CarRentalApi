using AutoMapper;
using User.Info.Model;
using Users.Entities;

namespace User.Info.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntity, UserInfo>().ReverseMap();
            CreateMap<UserEntity, UserInfoForGet>().ReverseMap();
        }
    }
}
