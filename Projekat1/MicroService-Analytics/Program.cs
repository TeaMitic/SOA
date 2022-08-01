using System.Text;
using System.Text.Json;
using MicroService_Analytics.DTO;
using MicroService_Analytics.MQTT;
using MQTTnet.Samples.Helpers;

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
                        var paylaod = e.ApplicationMessage.Payload; // u payload se nalazi data
                        var data = Encoding.Default.GetString(paylaod); //pretvaranje byte u string - dekodiranje podataka - data je jedan string
                        Agriculture agr = JsonSerializer.Deserialize<Agriculture>(data,_jsonSerializerOptions); //dobijanje podataka u konkretan objekat
                        await mqttClient.PublishEvent("analytics/data",agr);
                    }
                    else if (e.ApplicationMessage.Topic == "analytics/alerts") 
                    {
                        //send data to notification service via GRPC and persist to influx db
                        Console.WriteLine("Kuiper detected bad soil health.");
                        var paylaod = e.ApplicationMessage.Payload;
                        var data = Encoding.Default.GetString(paylaod);
                        Console.WriteLine(data);
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