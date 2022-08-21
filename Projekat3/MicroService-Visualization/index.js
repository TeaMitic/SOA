const mqtt = require('mqtt') 
let mqttClient = mqtt.connect('mqtt://mqtt-edgex:1883')
let dataTopic = 'agriculture/data'
const {InfluxDB} = require('@influxdata/influxdb-client')
const {Point} = require('@influxdata/influxdb-client')

//Influxdb setup
const token = "EIkanKPP4inoos5l9VDcVivClciX2hLWRGpVyicBVhN20_7meTcKGFNvJ3P5XS8dnk6CMKXt1isbh53R8x-GQA=="
const org = "organization"
const bucket = "visualization-bucket"
const influxClient = new InfluxDB({url: 'http://influx:8087', token: token})
let writeApi = influxClient.getWriteApi(org, bucket)
writeApi.useDefaultTags({tag: 'default'})



mqttClient.on('connect', () => { 
    mqttClient.subscribe(dataTopic, (err, granted) => { 
        if(err) console.log('Error while subscribing to ', dataTopic, 'err')
        else console.log(`Mqtt client subscribed to '${dataTopic}' topic`)
    })
})
  




//event listener for messages
mqttClient.on('message', async (topic, binMessage, packet) => { //binMessage == Binary message 
    //binMessage == binary payload == data sent over mqtt
    //packet == complete packet with other metadata about mqtt network and paylaod 
    if (topic === dataTopic) { 
        try {
            //new sensor reading 
            let reading = JSON.parse(binMessage.toString()).readings[0]
            const point = new Point('visualization-mes').tag('sensor',reading.name).floatField(reading.name, parseFloat(reading.value))
            writeApi.writePoint(point)
            console.log(point)
            //persist to influxdb
            
        } catch (error) {
            console.log(error)
        }
    }
})

