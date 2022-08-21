using Microsoft.AspNetCore.Mvc;
using MicroService_Gateway.Services;
using MicroService_Gateway.Models;
using MicroService_Gateway.DTO;
using Newtonsoft.Json;
using MicroService_Gateway.CSVWrapper;
using MicroService_Gateway.MQTT;

namespace MicroService_Gateway.Controllers;


[ApiController]
[Route("api/agriculture")]
public class AgricultureController : ControllerBase
{
   

    public AgricultureController() { }

    /// <summary>
    /// Generate data from csv file to mock iot sensor for agriculture data
    /// </summary>
    /// <param name="sleepSeconds">Time gap between two sensor readings</param>
    /// <returns></returns>
    /// <response code="200">Informs that data is being generated.</response>
    /// <response code="500">Informs that server error occured during generating the data.</response>
    [HttpPost]
    [Route("GenerateData/{sleepSeconds}")]
    public async Task<IActionResult> GenerateData([FromRoute] int sleepSeconds)
    {
        try
        {  
           
            CsvHelperWrapper csvWrapper = new CsvHelperWrapper("/home/datasets/Agriculture/Crop_recommendation.csv");
            int numOfItemsLoaded = 0;
            Agriculture? crop;
            using ( var mqttClient = new MQTTService()) 
            {
                while (!csvWrapper.Eof)
                {
                    crop =  csvWrapper.ReadMoreRecords<Agriculture?,AgricultureClassMap>(numOfItemsLoaded,1).FirstOrDefault();
                    if (crop == null) 
                    {
                        break;
                    }
                    numOfItemsLoaded++;

                    //sending via MQTT 
                    Console.WriteLine(JsonConvert.SerializeObject(crop));
                    //simulating the time gap in sensoring the data 
                    await mqttClient.PublishEvent("analytics/agriculture",crop); 
                    Thread.Sleep(sleepSeconds*1000); 

                }
            } 
            
            return StatusCode(200,"Generating data finished.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500,ex);
        }
    }
}