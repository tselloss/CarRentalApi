using RentInfo.Entities;
using Users.Entities;

namespace Postgres.Context.Entities
{
    public class ClientEntity : UserEntity
    {
        public virtual List<RentalEntity> Rents { get; set; } = new List<RentalEntity>();
        public virtual List<PreferenceEntity> Preferences { get; set; } = new List<PreferenceEntity>();
    }
}
