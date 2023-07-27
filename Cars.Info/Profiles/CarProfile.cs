using AutoMapper;
using Cars.Entities;
using Cars.Info.Model;

namespace CarRental.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarEntity, CarsInfo>();
        }
    }
}
