using CarRentalApi.Model;
using Cars.Entities;
using Microsoft.AspNetCore.Http;
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
        public List<String> ExcludedMonths { get; set; }

        public List<int> Rents { get; set; } = new List<int>();

        public static CarPresenter GetPresenter(CarEntity car) 
        {
            CarPresenter carPresenter = BuildPresenter(car);

            List<String> excludedDates = new List<string>();
            foreach (var rent in car.Rents)
            {
                DateTime dateFrom = DateTimeOffset.FromUnixTimeSeconds(rent.DateFrom).DateTime;
                DateTime dateTo = DateTimeOffset.FromUnixTimeSeconds(rent.DateTo).DateTime;
                while (dateFrom < dateTo)
                {
                    excludedDates.Add(Tools.GetStringDate(dateFrom));
                    if (dateFrom.Month == 12)
                    {
                        dateFrom = dateFrom.AddYears(1);
                    }
                    dateFrom = dateFrom.AddMonths(1);
                }
            }
            carPresenter.ExcludedMonths = excludedDates;
            return carPresenter;
        }
        public static List<CarPresenter> GetPresenter(List<CarEntity> cars)
        {
            List<CarPresenter> carsPresenter = new List<CarPresenter>();
            foreach (var car in cars)
            {
                carsPresenter.Add(BuildPresenter(car));
            }
            return carsPresenter;
        }
        private static CarPresenter BuildPresenter(CarEntity car)
        {
            List<int> rents = new List<int>();
            int AdminId = -1;
            if (car.Admin != null)
            {
                AdminId = car.Admin.UserId;
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
    }
}
