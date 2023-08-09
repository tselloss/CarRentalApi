using Postgres.Context.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomentation.Info.Response
{
    public class RecomentationPresenter
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Seats { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public int ClientId { get; set; }
        public static List<RecomentationPresenter> getPresenter(List<PreferenceEntity> preferenceEntities)
        {
            List<RecomentationPresenter> presenters = new List<RecomentationPresenter>();
            foreach (PreferenceEntity preferenceEntity in preferenceEntities)
            {
                presenters.Add(new RecomentationPresenter()
                {
                    Id = preferenceEntity.Id,
                    Brand = preferenceEntity.Brand,
                    Model = preferenceEntity.Model,
                    Price = preferenceEntity.Price,
                    Color = preferenceEntity.Color,
                    Status = preferenceEntity.Status,
                    ClientId = preferenceEntity.Client.UserId,
                    Seats = preferenceEntity.Seats
                });
            }
            return presenters;
        }
    }
}
