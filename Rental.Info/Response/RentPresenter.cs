using Cars.Info.Responses;
using RentInfo.Entities;

namespace Rental.Info.Response
{
    public class RentPresenter
    {
        public int RentalId { get; set; }
        public long DateFrom { get; set; }
        public long DateTo { get; set; }
        public int Value { get; set; }
        public int ClientId { get; set; }
        public CarPresenter Car { get; set; }
        
        public static RentPresenter getPresenter(RentalEntity rentEntity)
        {
            return new RentPresenter()
            {
                RentalId = rentEntity.RentalId,
                DateFrom = rentEntity.DateFrom,
                DateTo = rentEntity.DateTo,
                Value = rentEntity.Value,
                ClientId = rentEntity.Client.UserId,
                Car = CarPresenter.BuildPresenter(rentEntity.Car)
            };
        }
        public static List<RentPresenter> getPresenter(List<RentalEntity> rents)
        {
            List<RentPresenter> presenters = new List<RentPresenter>();
            foreach (RentalEntity entity in rents)
            {
                presenters.Add(getPresenter(entity));
            }
            return presenters;
        }
    }
}
