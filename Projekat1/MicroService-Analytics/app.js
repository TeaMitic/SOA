const express = require('express')
const mongoose = require('mongoose')

const app = express()
app.use(express.json())
app.use(express.urlencoded({extended:false}))

const dbURI = 'mongodb://localhost:27017/soa-analytics' //local mongodb instance 
mongoose.connect(dbURI, { useNewUrlParser: true, useUnifiedTopology: true})
  .then((result) =>{
    console.log('Mongo instance is running.');
    app.listen(5001,() => {
      console.log('Server is listening on port 5001...');
    });
  })
  .catch((err) => console.log(err));