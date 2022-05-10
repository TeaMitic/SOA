const mongoose= require('mongoose')
const Song = require('../models/songModel')

//functions

const AddOne = async (req,res)=>{
    try {
        //can be put in separe function createOne
        const song = await Song.create({
            trackName: req.body.trackName,
            artistName: req.body.artistName,
            genre: req.body.genre,
            beatsPerMinute: req.body.beatsPerMinute,
            energy: req.body.energy,
            danceability: req.body.danceability,
            loudnesIndB: req.body.loudnesIndB,
            liveness: req.body.liveness,
            valence: req.body.valence,
            lenght: req.body.lenght,
            acousticness: req.body.acousticness,
            speechiness: req.body.speechiness,
            popularity: req.body.popularity
        })
        let sendInfo ={
            id: song._id,
            trackName: song.trackName,
            artistName: song.artistName
        }
        res.status(200).send(sendInfo)
        
    } catch (error) {
        res.status(500).send(error.message)
    }
}

const AddMany = async (req,res)=>{
    try { 
        let result = await Song.insertMany(req.body)
        if (!result) { 
            let sendInfo = "Error while adding songs."
            return res.status(400).send(sendInfo)            
        }
        sendInfo = "Songs successfully added."
        res.status(200).send(sendInfo)
    }
    catch(error) { 
        res.status(500).send(error.message)
    }
   
}

const GetOne = async (req,res)=>{
    try {
        let result = await Song.findOne({trackName : req.params.track , artistName: req.params.artist})
        let sendInfo= ""

        if(!result){
            sendInfo = "Error: Song not found"
            return res.status(400).send(sendInfo)
        }
        console.log(result)
        sendInfo = "Song successfully found"
        res.status(200).send(result)

    } catch (error) {
        
    }
}

module.exports ={
    AddOne,
    AddMany,
    GetOne
}

