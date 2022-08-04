const mqtt = require('mqtt') 
const mqttClient = mqtt.connect("mqtt://mqtt-edgex:1883/")
const axios = require('axios')

let dataTopic = 'environment-data'
mqttClient.on('connect', () => { 
    mqttClient.subscribe(dataTopic, (err, granted) => { 
      if(err) console.log('Error while subscribing to ', dataTopic, 'err')
      else console.log('Mqtt client subscribed ', dataTopic ,'topic')
    })
  })
  
mqttClient.on("error", function(err) { 
    console.log("Error: " + err) 
    if(err.code == "ENOTFOUND") { 
        console.log("Network error, make sure you have an active internet connection") 
    } 
}) 

mqttClient.on("close", function() { 
    console.log("Connection closed by client") 
}) 

mqttClient.on("reconnect", function() { 
    console.log("Client trying a reconnection") 
}) 

mqttClient.on("offline", function() { 
    console.log("Client is currently offline") 
})

//event listener for messages
mqttClient.on('message', async (topic, binMessage, packet) => { //binMessage == Binary message 
if (topic === dataTopic) { 
    console.log("New message.")
    //convert message
    //check for irregular data
    //send command if irregular data detected
}
})

