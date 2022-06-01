const express = require('express')
const mongoose = require('mongoose')
const swaggerUI =require('swagger-ui-express')
const swaggerJsDoc = require('swagger-jsdoc')
const options = require('./api-doc')

const songs= require('./routes/songsRoutes')
const specs = swaggerJsDoc(options)

const app= express()
app.use(express.json())
app.use(express.urlencoded({extended:false}))
// console.log(options)
app.use("/api-docs", swaggerUI.serve, swaggerUI.setup(specs))

app.use('/api/songs', songs)

const port  = process.env.PORT || 5000

const dbURI = 'mongodb+srv://soa-projekat1:SOAProjekat1@soa-projekat1.0os6d.mongodb.net/SongsDB?retryWrites=true&w=majority'; //atlas - cloud mongodb instance
// const dbURI = 'mongodb://192.168.1.199:27017/soa-songs' //local mongodb instance 
mongoose.connect(dbURI, { useNewUrlParser: true, useUnifiedTopology: true})
  .then((result) =>{
    console.log('Mongo instance is running.');
  })
  .catch((err) => console.log(err));
app.listen(port,() => {
  console.log(`Server is listening on port ${port}...`);
});