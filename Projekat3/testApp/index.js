const express = require('express')

const app= express()
app.use(express.json())
app.use(express.urlencoded({extended:false}))

let waterPumpWorking = false

app.put("/api/waterPump", (req, res) => {
    waterPumpWorking = req.body.working
    if(waterPumpWorking) console.log("Water pump started working.")
    else if (!waterPumpWorking) console.log("Water pump stopped working.")
    else { res.sendStatus(400); return }
    res.sendStatus(201)
})

app.get("/api/waterPump", (req, res) => {
    let pumpState = waterPumpWorking ? "working" : "not working"
    res.status(200).send(`Water pump is ${pumpState}.`)
})

port = 8088
app.listen(port, () => {
    console.log("Server is listening on port: ", port)
})