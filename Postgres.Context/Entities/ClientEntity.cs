using RentInfo.Entities;
using Users.Entities;

namespace Postgres.Context.Entities
{
    public class ClientEntity : UserEntity
    {
        public IEnumerable<RentalEntity> Rents { get; set; }
    }
}
