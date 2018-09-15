using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileCoverageMapApp.Models
{
    public class MobileCoverageMapAppViewModel
    {
        public List<Measurement> Measurements { get; set; }
    }

    public class Measurement
    {
        public DateTime Time { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
        public float RSRP { get; set; }
        public string ContentString { get; set; }
    }
}