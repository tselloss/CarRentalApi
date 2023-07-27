using AutoMapper;
using Cars.Entities;
using Cars.Model;

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
