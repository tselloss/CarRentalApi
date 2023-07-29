using AutoMapper;
using RentInfo.Entities;
using RentInfo.Model;

namespace CarRental.Profiles
{
    public class RentalProfile : Profile
    {
        public RentalProfile()
        {
            CreateMap<RentalEntity, RentalInfo>().ReverseMap();
        }
    }
}
