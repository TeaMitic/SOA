const express = require('express')
const mongoose = require('mongoose')

const songs= require('./routes/songsRoutes')

const app= express()
app.use(express.json())
app.use(express.urlencoded({extended:false}))

app.use('/api/songs', songs)



const dbURI = 'mongodb+srv://soa-projekat1:SOAProjekat1@soa-projekat1.0os6d.mongodb.net/SongsDB?retryWrites=true&w=majority';
mongoose.connect(dbURI, { useNewUrlParser: true, useUnifiedTopology: true})
  .then((result) =>{
  app.listen(5000,() => {
    console.log('Server is listening on port 5000...');
    });
  })
  .catch((err) => console.log(err));