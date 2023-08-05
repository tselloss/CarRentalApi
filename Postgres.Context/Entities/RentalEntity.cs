using Cars.Entities;
using Postgres.Context.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Users.Entities;

namespace RentInfo.Entities
{
    public class RentalEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RentalId { get; set; }
        [Required]
        public long DateFrom { get; set; }
        [Required]
        public long DateTo { get; set; }
        [Required]
        public int Value { get; set; }
        public virtual ClientEntity Client { get; set; }
        public virtual CarEntity Car { get; set; }
    }
}
