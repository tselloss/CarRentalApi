using Cars.Entities;
using Users.Entities;

namespace Postgres.Context.Entities
{
    public class AdminEntity : UserEntity
    {
        public IEnumerable<CarEntity> Cars { get; set; }
    }
}
