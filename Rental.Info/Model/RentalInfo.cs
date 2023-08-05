using Cars.Info.Model;
using System.ComponentModel.DataAnnotations;
using User.Info.Model;

namespace RentInfo.Model
{
    public class RentalInfo
    {
        public UserInfo User { get; set; }
        public CarsInfo Cars { get; set; }
        public int Value { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
