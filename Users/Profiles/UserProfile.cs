using AutoMapper;

namespace Users.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Entities.UserEntity, Model.UserInfo>();
            CreateMap<Model.UserInfo, Entities.UserEntity>();
        }
    }
}
