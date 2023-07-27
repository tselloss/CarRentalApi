using Cars.Model;
using Users.Model;

namespace RentInfo.Model
{
    public class RentalInfo
    {
        public UserInfo User { get; set; }
        public CarsInfo Car { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
