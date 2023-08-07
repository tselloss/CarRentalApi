using Cars.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Postgres.Context.Entities
{
    public class PreferenceEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int? Seats { get; set; }
        public float? Price { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public virtual ClientEntity Client { get; set; }
    }
}
