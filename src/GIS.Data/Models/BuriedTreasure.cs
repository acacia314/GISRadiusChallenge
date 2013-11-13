using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace GIS.Data.Models
{
    public class BuriedTreasure
    {
        public int _id { get; set; }
        public int t { get; set; }
        public GeoJSON l { get; set; }
    }
}
