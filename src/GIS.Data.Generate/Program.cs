using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using GIS.Data.Models;
namespace GIS.Data.Generate
{
    class Program
    {
        static private Random random;
        static void Main(string[] args)
        {
            random = new Random();
            const string connectionString = "mongodb://testuser:testuser@ds053658.mongolab.com:53658/realcrowdgis";
            var client = new MongoClient(connectionString);
            var server = client.GetServer();
            var database = server.GetDatabase("realcrowdgis");
            var collection = database.GetCollection<BuriedTreasure>("buriedtreasure");

            for (var i = 0; i < 100; i++)
            {
                collection.InsertBatch(GenerateTreasure(i));
                
            }

        }

        
        static IEnumerable<BuriedTreasure> GenerateTreasure(int batchNumber)
        {
            var treasure = new BuriedTreasure[100000];
            var batchBase = batchNumber*100000;
            for (var i = 0; i < 100000; i++)
            {
                treasure[i] = new BuriedTreasure()
                    {
                        _id = batchBase+i,
                        t = random.Next(0, 3),
                        l = new GeoJSON()
                            {
                                type = "Point",
                                coordinates = RandomLocation()
                            }
                    };
            }
            return treasure;
        }

        static double[] RandomLocation()
        {
            return new double[2]
            {
                random.NextDouble()*360-180, //Longitude
                random.NextDouble()*180-90 //Latitude
            };
        
        }
    }
}
