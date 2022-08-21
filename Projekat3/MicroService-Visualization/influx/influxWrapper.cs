using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

namespace MicroService_Visualization.InfluxDB 
{
    public static class InfluxWrapper 
    {
        private static string _token_dimitrije = "oLOGcEUZEG-PwnHc4kCzdqghRv9dFy9jyrI0g6-NpTCmBzZ78u_uFlHLdbDf7xtYzaeLAOa9ppenoOmp_hiFLA=="; //dimitrije
        private static string _token_tea = "eSy0_-op61hsHt9jeDdBAYsyy3XR0N4VzyrUwbz0fwPg3muYH_U8Sx5N0ejbYMa-CMTXFf-0UsF8uMwOlqBavg=="; //tea
        private static string _bucket = "visualization-bucket";
        private static string _org = "organization";

        private static InfluxDBClient? _client = null;       

        public static bool Configure(bool isTeaToken) { 
            try
            {
                if (_client != null) { return true; }
                var token = isTeaToken? _token_tea : _token_dimitrije;
                _client = InfluxDBClientFactory.Create("http://influx:8086", token);                                    
                return true;
                
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error in Configure method.");
                throw;
            }
        }

        public async static Task<Boolean> InsertData(PointData point) { 
            try
            {
                if (_client == null) { 
                    throw new Exception("InfluxDB Client not configured. Invoke 'Configure' function before inserting data.");
                }
                var writeApiAsync = _client.GetWriteApiAsync();
                await writeApiAsync.WritePointAsync(point, _bucket, _org); 
                return true;
                
            }
            catch (System.Exception)
            {
                Console.WriteLine("Error in Configure method.");
                throw;
            }

        }
    


    }
}