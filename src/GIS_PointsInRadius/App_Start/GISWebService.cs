using System;
using System.Collections;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
using GIS.Data;
using GIS.Data.Models;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.Common;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.WebHost.Endpoints;

namespace GIS_PointsInRadius
{
	//Request DTO
    
	public class WithinRadiusRequest
	{
		public double radius { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public int? type { get; set; }
	}

	//Response DTO
    public class WithinRadiusResponse
	{
		public IEnumerable<BuriedTreasure> Results { get; set; }
		public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
	}

	//Can be called via any endpoint or format, see: http://servicestack.net/ServiceStack.Hello/
	public class GISService : Service
	{
        public object Get(WithinRadiusRequest request)
        {
            var dataContext = new DB();
            IEnumerable<BuriedTreasure> treasures;
            treasures = request.type.HasValue ? 
                dataContext.TreasureWithinRadius(request.radius, request.latitude, request.longitude,request.type.Value) 
                : dataContext.TreasureWithinRadius(request.radius, request.latitude, request.longitude);

            return new WithinRadiusResponse { Results = treasures };
		}
	}

  


}
