using System.Text;
using System.Text.Json;
using MicroService_Analytics.DTO;
using MicroService_Analytics.MQTT;
using MQTTnet.Samples.Helpers;
using MicroService_Analytics.gRPC;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;
using GrpcClient;
using MicroService_Analytics.InfluxDB;

internal class Program
{
   
    private static async Task Main(string[] args)
    {
        try 
        {
            int count  = 0;
            using (var mqttClient = new MQTTService())
            {
                await mqttClient.Connect();
                await mqttClient.SubscribeTopic("analytics/agriculture");
                await mqttClient.SubscribeTopic("analytics/alerts");
                JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                //grpc deo
                //listener for messages from mqtt    
                mqttClient.Listener.ApplicationMessageReceivedAsync += async (e) =>  {
                    if (e.ApplicationMessage.Topic == "analytics/agriculture")
                    {
                        //send data to ekuiper
                        var payload = e.ApplicationMessage.Payload; // u payload se nalazi data
                        var data = Encoding.Default.GetString(payload); //pretvaranje byte u string - dekodiranje podataka - data je jedan string
                        await mqttClient.PublishEvent("analytics/data",data);
                    }
                    else if (e.ApplicationMessage.Topic == "analytics/alerts") 
                    {

                        
                        try{
                            Console.WriteLine("Kuiper detected bad soil health.");
                            count++;
                            var payload = e.ApplicationMessage.Payload;
                            var data = Encoding.Default.GetString(payload).Replace('[',' ').Replace(']',' ').Trim();
                            Console.WriteLine(data);
                            Agriculture agr = JsonSerializer.Deserialize<Agriculture>(data,_jsonSerializerOptions);

                            // persisting data into influxdb
                            
                            PointData point = PointData
                            .Measurement("analytics-bucket")
                            .Tag("cropType", agr.CropType)
                            .Field("ph", agr.Ph)
                            .Field("nitrogen", agr.Nitrogen)
                            .Field("phosphorus", agr.Phosphorus)
                            .Field("potassium", agr.Potassium )
                            .Field("temperature", agr.Temperature)
                            .Field("humidity", agr.Humidity )
                            .Field("rainfall", agr.Rainfall)
                            .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

                            InfluxWrapper.Configure(false); //setting Dimitrije's token for influx
                            await InfluxWrapper.InsertData(point);
                            //grpc deo 
                            Console.WriteLine("Started sending over gRPC");
                            var grpcClient = gRPCWrapper.GRPCClient;

                            var responses =  grpcClient.sendNotif(new Notif {
                                Nitrogen = agr.Nitrogen, 
                                Phosphorus = agr.Phosphorus, 
                                Potassium = agr.Potassium,
                                Temperature = (float)agr.Temperature,
                                Humidity = (float)agr.Humidity,
                                Ph = (float)agr.Ph,
                                Rainfall = (float)agr.Rainfall,
                                CropType = agr.CropType
                            },null);  
                            Console.WriteLine("Finished sending over gRPC");
                        }
                         catch(Exception ex) 
                        {
                            Console.WriteLine(ex);
                        }
                        
                    }
                    // e.DumpToConsole();
                    return;
                };
                Console.WriteLine("Service started. Press any key to exit.");
                while(true)
                { 
                    var input = Console.ReadLine();
                    if (input != null) 
                    {
                        Console.WriteLine($"Loaded {count} plants.");
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
