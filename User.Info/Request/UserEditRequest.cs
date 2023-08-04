using CarRentalManagment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Info.Request
{
    public class UserEditRequest
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
    }
}
