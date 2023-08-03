using Cars.Entities;
using Users.Entities;

namespace Postgres.Context.Entities
{
    public class AdminEntity : UserEntity
    {
        public virtual List<CarEntity> Cars { get; set; } = new List<CarEntity> ();
    }
}
