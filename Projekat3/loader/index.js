const axios = require('axios')
const fs = require('fs')
const { parse } = require('csv-parse')
const parser = parse({columns: true}, async (err,records) => {
    var config = {
        headers: {
            'Content-Type': 'text/plain'
        },
       responseType: 'text'
    }
    for await  (element of records)  {
        console.log(`Sensor detected: - Soil Humidity = [${element.soilHumidity}] - Air temperature = [${element.airTemperature}]`)
        await axios.post('http://localhost:49986/api/v1/resource/Temp_and_Humidity_sensor_cluster_01/temperature', element.airTemperature, config)
        await axios.post('http://localhost:49986/api/v1/resource/Temp_and_Humidity_sensor_cluster_01/humidity', element.soilHumidity, config)
        await new Promise(r => setTimeout(r,2000))
    };
})  

fs.createReadStream('../Datasets/TARP.csv').pipe(parser)