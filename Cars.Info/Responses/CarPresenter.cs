using Cars.Entities;
using Postgres.Context.Entities;
using RentInfo.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Info.Responses
{
    internal class CarPresenter
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Seats { get; set; }
        public float Price { get; set; }
        public string Image { get; set; }
        public int AdminId { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }

        public List<int> Rents { get; set; } = new List<int>();

        public static CarPresenter GetPresenter(CarEntity car) 
        {
            int AdminId = -1;
            if (car.Admin != null)
            {
                AdminId = car.Admin.UserId;
            }
            List<int> rents = new List<int>();
            foreach (var rent in car.Rents)
            {
                rents.Add(rent.RentalId);
            }

            return new CarPresenter()
            {
                CarId = car.CarId,
                Brand = car.Brand,
                Model = car.Model,
                Seats = car.Seats,
                Price = car.Price,
                Image = car.Image,
                Color = car.Color,
                Status = car.Status,
                AdminId = AdminId,
                Rents = rents
            };
        }
        public static List<CarPresenter> GetPresenter(List<CarEntity> cars)
        {
            List<CarPresenter> carsPresenter = new List<CarPresenter>();
            foreach (var car in cars)
            {
                carsPresenter.Add(CarPresenter.GetPresenter(car));
            }
            return carsPresenter;
        }
    }
}
