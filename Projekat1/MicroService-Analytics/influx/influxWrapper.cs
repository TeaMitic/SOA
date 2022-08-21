using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace MicroService_Analytics.InfluxDB 
{
    public static class InfluxWrapper 
    {
        private static string _token_didi = "ZAZNBrr5QBzgE3BZe6MNbavEWY24iHEnwSEnj_QTmlj166zfdmy7_tdggBo7RjHmEmlyVvHrlgoqqbbfhKE2iQ=="; //dimitrije
        private static string _token_tea = "eSy0_-op61hsHt9jeDdBAYsyy3XR0N4VzyrUwbz0fwPg3muYH_U8Sx5N0ejbYMa-CMTXFf-0UsF8uMwOlqBavg=="; //tea
        private static string _bucket = "analytics-bucket";
        private static string _org = "organization";

        private static InfluxDBClient _client;

        private static InfluxDBClient InfluxClient 
        {
            get {
                if (_client == null) { 
                    _client = Configure(true);
                }
                return _client;
            }
        }
       

        public static InfluxDBClient Configure(bool isTeaToken) { 
            var token = isTeaToken? _token_tea : _token_didi;
            return  InfluxDBClientFactory.Create("http://influxdb:8086", token);                                    
            
        }

        public async static Task InsertData(PointData point) { 
            var writeApiAsync = InfluxClient.GetWriteApiAsync();
            await writeApiAsync.WritePointAsync(point, _bucket, _org); 

        }
    


    }
}