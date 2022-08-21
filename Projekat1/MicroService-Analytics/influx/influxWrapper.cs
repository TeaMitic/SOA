using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace MicroService_Analytics.InfluxDB 
{
    public static class InfluxWrapper 
    {
        private static string _token_dimitrije = "ZAZNBrr5QBzgE3BZe6MNbavEWY24iHEnwSEnj_QTmlj166zfdmy7_tdggBo7RjHmEmlyVvHrlgoqqbbfhKE2iQ=="; //dimitrije
        private static string _token_tea = "eSy0_-op61hsHt9jeDdBAYsyy3XR0N4VzyrUwbz0fwPg3muYH_U8Sx5N0ejbYMa-CMTXFf-0UsF8uMwOlqBavg=="; //tea
        private static string _bucket = "analytics-bucket";
        private static string _org = "organization";

        private static InfluxDBClient _client = null;       

        public static bool Configure(bool isTeaToken) { 
            if (_client != null) { return true; }
            var token = isTeaToken? _token_tea : _token_dimitrije;
            _client = InfluxDBClientFactory.Create("http://influxdb:8086", token);                                    
            return true;
        }

        public async static Task<Boolean> InsertData(PointData point) { 
            if (_client == null) { 
                throw new Exception("InfluxDB Client not configured. Invoke 'Configure' function before inserting data.");
            }
            var writeApiAsync = _client.GetWriteApiAsync();
            await writeApiAsync.WritePointAsync(point, _bucket, _org); 
            return true;

        }
    


    }
}