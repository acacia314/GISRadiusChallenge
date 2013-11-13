using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GIS.Data.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Wrappers;

namespace GIS.Data
{
    public class DB
    {
        private static MongoCollection _collection ;
        public DB()
        {
            const string connectionString = "mongodb://testuser:testuser@ds053658.mongolab.com:53658/realcrowdgis";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("realcrowdgis");
            _collection = database.GetCollection<BuriedTreasure>("buriedtreasure");
        }

        public IEnumerable<BuriedTreasure> TreasureWithinRadius(double radius, double latitute, double longitude)
        {
            var jsonQuery = String.Format("{{ l :{{ $geoWithin : {{ $centerSphere :  [ [ {0} , {1} ] , {2} ] }} }} }} ",longitude,latitute,radius/3959);
            BsonDocument doc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonQuery);
           
            
            return _collection.FindAs<BuriedTreasure>(new QueryWrapper(doc));
        }
        public IEnumerable<BuriedTreasure> TreasureWithinRadius(double radius, double latitute, double longitude, int type)
        {

            var jsonQuery = String.Format("{{ l :{{ $geoWithin : {{ $centerSphere :  [ [ {0} , {1} ] , {2} ] }} }},t:{3} }} ", longitude, latitute, radius / 3959,type);
            BsonDocument doc = MongoDB.Bson.Serialization.BsonSerializer.Deserialize<BsonDocument>(jsonQuery);


            return _collection.FindAs<BuriedTreasure>(new QueryWrapper(doc));
        }

    }
}
