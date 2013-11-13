using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIS.Data.Models
{
    public class GeoJSON
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }
}
