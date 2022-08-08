const express = require('express')

const app= express()
app.use(express.json())
app.use(express.urlencoded({extended:false}))

let waterPumpWorking = false

app.put("/api/waterPump", (req, res) => {
    if (req.body.working !== undefined) { 
        waterPumpWorking = req.body.working
        let pumpState = waterPumpWorking ? "started" : "stopped"
        let msg = `Water pump ${pumpState} working.`
        console.log(`PUT ACTION: ${msg}`)
        return res.status(200).send(msg)
    }
    return res.status(400).send("Argument 'working' not found!");
    
})

app.get("/api/waterPump", (req, res) => {
    let pumpState = waterPumpWorking ? "working" : "not working"
    let msg = `Water pump is ${pumpState}.`
    console.log(`GET ACTION: ${msg}`)
    res.status(200).send({
        working: waterPumpWorking,
        message: msg
    })
})

port = 8088
app.listen(port, () => {
    console.log("Server is listening on port: ", port)
})