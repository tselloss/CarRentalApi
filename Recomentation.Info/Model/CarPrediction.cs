using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recomentation.Info.Model
{
    public class CarPrediction
    {
        [KeyType(count: 100)] // Maximum number of brands
        public string Brand { get; set; }
        [KeyType(count: 1000)] // Maximum number of models
        public string Model { get; set; }
        public float Score { get; set; }

    }
}
