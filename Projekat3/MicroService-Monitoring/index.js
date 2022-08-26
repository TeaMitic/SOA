const mqtt = require('mqtt') 
let mqttClient = mqtt.connect('mqtt://mqtt-edgex:1883')
const axios = require('axios')
let dataTopic = 'agriculture/data'

mqttClient.on('connect', () => { 
    mqttClient.subscribe(dataTopic, (err, granted) => { 
        if(err) console.log('Error while subscribing to ', dataTopic, 'err')
        else console.log(`Mqtt client subscribed to '${dataTopic}' topic`)
    })
})
  
const togglePumpState = async (pumpWorking) => { 
    try {
        var result = await axios.put(pumpApi_put, 
            {
                "working" : pumpWorking
            })
        console.log(`Action done: - ${result.data}`) 
    } catch (error) {
        throw error
    }
}

let humidityLowLevel = 30
let humidityHighLevel = 55
let humiditySensor = 'humidity'

// let pumpApi_get = "http://edgex-core-command:48082/api/v1/device/64cace74-c4e8-4c18-b631-fa88531607ac/command/33f13ffe-b4a5-4527-a0a7-c4fd45e84124" //tea
// let pumpApi_put = "http://edgex-core-command:48082/api/v1/device/64cace74-c4e8-4c18-b631-fa88531607ac/command/33f13ffe-b4a5-4527-a0a7-c4fd45e84124" //tea
let pumpApi_get = `http://edgex-core-command:48082/api/v1/device/9e367135-e1e3-4ca6-8371-af152b7f2f10/command/147442ba-a3e9-4106-b687-7ccd2c604bbb` //dimitrije
let pumpApi_put = "http://edgex-core-command:48082/api/v1/device/9e367135-e1e3-4ca6-8371-af152b7f2f10/command/147442ba-a3e9-4106-b687-7ccd2c604bbb" //dimitrije
// let pumpApi_get = `http://host.docker.internal:8088/api/waterPump`
// let pumpApi_put = `http://host.docker.internal:8088/api/waterPump`

//event listener for messages
mqttClient.on('message', async (topic, binMessage, packet) => { //binMessage == Binary message 
    //binMessage == binary payload == data sent over mqtt
    //packet == complete packet with other metadata about mqtt network and paylaod 
    if (topic === dataTopic) { 
        try {
            //new sensor reading 
            let reading = JSON.parse(binMessage.toString()).readings[0]

            //only humidity readings are important
            if (reading.name === humiditySensor) {
                console.log(`Sensor readings: ${reading.name} - ${reading.value}`)

                //get pump state 
                let pumpResult = await axios.get(pumpApi_get)
                let pumpData = pumpResult.data
                console.log(`Current pump state: - ${pumpData.message}`)

                //pump working? 
                if (pumpData.working) { 
                    //pump - on

                    //high humidity ? => turn pump off 
                    if (parseInt(reading.value) > humidityHighLevel) { 
                        await togglePumpState(false)
                    }
                }
                else {
                    //pump - off

                    //low humidity ? => turn pump on 
                    if (parseInt(reading.value) < humidityLowLevel) { 
                       await togglePumpState(true)
                    }
                }
            }
            
        } catch (error) {
            console.log(error)
        }
    }
})

