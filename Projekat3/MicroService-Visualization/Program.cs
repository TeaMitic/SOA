using System.Text;
using System.Text.Json;
using MicroService_Visualization.MQTT;
using MQTTnet.Samples.Helpers;
using MicroService_Visualization.InfluxDB;
using InfluxDB.Client.Writes;
using InfluxDB.Client.Api.Domain;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

internal class Program
{
   
    private static async Task Main(string[] args)
    {
        string mqttTopic = "agriculture/data";
        try 
        {
            using (var mqttClient = new MQTTService())
            {
                await mqttClient.Connect();
                await mqttClient.SubscribeTopic(mqttTopic);
                JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                //influx config 
                InfluxWrapper.Configure(false); //setting Dimitrije's token for influx

                //listener for messages from mqtt    
                mqttClient.Listener.ApplicationMessageReceivedAsync +=  async (e) =>  {
                    if (e.ApplicationMessage.Topic == mqttTopic) 
                    {
                        try{
                            var payload = e.ApplicationMessage.Payload;
                            // var data = Encoding.Default.GetString(payload).Replace('[',' ').Replace(']',' ').Trim();
                            var dataStr = Encoding.Default.GetString(payload);
                            dynamic? json = JsonConvert.DeserializeObject(dataStr);
                            var data = json.readings[0];
                            Console.WriteLine(json.readings[0].name);
                            // Agriculture agr = JsonSerializer.Deserialize<Agriculture>(data,_jsonSerializerOptions);

                            // persisting data into influxdb                            
                            string value = Convert.ToString(data.value);
                            Console.WriteLine(value);
                            PointData point = PointData
                            .Measurement("visualization-bucket")
                            .Tag($"sensor", $"{data.name}")
                            .Field($"{data.name}", float.Parse(value))
                            .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                            await InfluxWrapper.InsertData(point);
                        }
                         catch(Exception ex) 
                        {
                            Console.WriteLine(ex);
                        }
                        
                    }
                    return;
                };
                Console.WriteLine("Service started. Press any key to exit.");
                while(true)
                { 
                    var input = Console.ReadLine();
                    if (input != null) 
                    {
                        break;
                    }
                }
                

            }
            
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex);
        }
    }
    
}
