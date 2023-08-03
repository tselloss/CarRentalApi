using Cars.Entities;
using Postgres.Context.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentInfo.Entities;

namespace Rental.Info.Response
{
    public class RentPresenter
    {
        public int RentalId { get; set; }
        public long DateFrom { get; set; }
        public long DateTo { get; set; }
        public int ClientId { get; set; }
        public int CarId { get; set; }
        
        public static RentPresenter getPresenter(RentalEntity rentEntity)
        {
            return new RentPresenter()
            {
                RentalId = rentEntity.RentalId,
                CarId = rentEntity.Car.CarId,
                ClientId = rentEntity.Client.UserId,
                DateFrom = rentEntity.DateFrom,
                DateTo = rentEntity.DateTo
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
