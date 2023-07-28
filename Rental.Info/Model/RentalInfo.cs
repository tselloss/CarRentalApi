using Cars.Info.Model;
using User.Info.Model;

namespace RentInfo.Model
{
    public class RentalInfo
    {
        public int RentalId { get; set; }
        public UserInfo User { get; set; }
        public CarsInfo Car { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
