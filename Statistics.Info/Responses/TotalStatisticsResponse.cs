using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Statistics.Info.Responses
{
    public class TotalStatisticsResponse
    {
        public int TotalPriceRents { get; set; }
        public int TotalRent { get; set; }
        public int TotalMonths { get; set; }
        public string Favourite { get; set; }
    }
}
