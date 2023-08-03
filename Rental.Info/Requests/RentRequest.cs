using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental.Info.Requests
{
    public class RentRequest
    {
        public int CarId { get; set; }
        public long DateFrom { get; set; }
        public long DateTo { get; set; }
    }
}
