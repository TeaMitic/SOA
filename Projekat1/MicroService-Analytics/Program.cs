using System.Text;
using System.Text.Json;
using MicroService_Analytics.DTO;
using MicroService_Analytics.MQTT;
using MQTTnet.Samples.Helpers;
using Grpc.Net.Client;
using GrpcClient;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Writes;

internal class Program
{
   
    private static async Task Main(string[] args)
    {
        try 
        {
            using (var mqttClient = new MQTTService())
            {
                await mqttClient.Connect();
                await mqttClient.SubscribeTopic("analytics/agriculture");
                await mqttClient.SubscribeTopic("analytics/alerts");
                JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
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
                        //send data to notification service via GRPC and persist to influx db
                        // Console.WriteLine("usao u alerts if");
                        // var paylaod = e.ApplicationMessage.Payload;
                        // Console.WriteLine("napravljen payload");
                        // Console.WriteLine(paylaod);
                        // var data = Encoding.Default.GetString(paylaod);
                        // Console.WriteLine("napravljen data");
                        // Console.WriteLine(data);
                        // Agriculture agr = JsonSerializer.Deserialize<Agriculture>(data,_jsonSerializerOptions);
                        // Console.WriteLine("napravljen objekat");
                        // Console.WriteLine(agr.Temperature);
                        Console.WriteLine("Kuiper detected bad soil health.");
                        var payload = e.ApplicationMessage.Payload;
                        var data = Encoding.Default.GetString(payload).Replace('[',' ').Replace(']',' ').Trim();
                        Console.WriteLine(data);
                        Agriculture agr = JsonSerializer.Deserialize<Agriculture>(data,_jsonSerializerOptions);

                        //persisting data into influxdb
                        // const string token = "b37i9XrzhdyD5vjZt0UmlLRIi4ROki4slLcPPnoaXK5lQWr6ex2HDdKKbBogh9cc1DwFXclWT-kwii2wFbWM7w==";
                        // const string bucket = "analytics-bucket";
                        // const string org = "organization";

                        // using var influxClient = InfluxDBClientFactory.Create("http://localhost:8086", token);
                        // // const string influxData = "mem,host=host1 used_percent=23.43234543";
                        // // using (var writeApi = influxClient.GetWriteApi())
                        // // {
                        // //     writeApi.WriteRecord(bucket,  WritePrecision.Ns, org, influxData); //ovde sam zamenila mesta writePercision i org 
                        // //     Console.WriteLine("Upisano u bazu");
                        // // }
                        // var point = PointData
                        // .Measurement("mem")
                        // .Tag("host", "host1")
                        // .Field("nitrogen", agr.Nitrogen )
                        // .Field("phosphorus", agr.Phosphorus)
                        // .Field("potassium", agr.Potassium )
                        // .Field("temperature", agr.Temperature)
                        // .Field("humidity", agr.Humidity )
                        // .Field("ph", agr.Ph)
                        // .Field("rainfall", agr.Rainfall)
                        // .Field("cropType", agr.CropType)
                        // .Timestamp(DateTime.UtcNow, WritePrecision.Ns);


                        // using (var writeApi = influxClient.GetWriteApi())
                        // {
                        //     writeApi.WritePoint(point, org, bucket); //zamenjena mesta bucket i point
                        // }


                        //grpc deo
                        // using var channel = GrpcChannel.ForAddress("https://localhost:8085");
                        // var grpcClient = new GrpcClient.Notification.NotificationClient(channel);
                        // var reply = await grpcClient.sendNotifAsync(new Notif {
                        //     Nitrogen = agr.Nitrogen, 
                        //     Phosphorus = agr.Phosphorus, 
                        //     Potassium = agr.Potassium,
                        //     Temperature = (float)agr.Temperature,
                        //     Humidity = (float)agr.Humidity,
                        //     Ph = (float)agr.Ph,
                        //     Rainfall = (float)agr.Rainfall,
                        //     CropType = agr.CropType
                        // });
                        // Console.WriteLine("Send notif to notification microsevice" + reply.Res);
                        
                    }
                    // e.DumpToConsole();
                    return;
                };
                Console.WriteLine("Service started. Press any key to exit.");
                while(true)
                { 
                    var input = Console.ReadLine();
                    if (input != null) break;
                }
                

            }
            
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex);
        }
    }
}