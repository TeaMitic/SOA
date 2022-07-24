const mongoose= require('mongoose')
const Song = require('../models/songModel')


//functions
const getSongJson = (body) => { 
    return { 
        trackName: body.trackName,
        artistName: body.artistName,
        genre: body.genre,
        beatsPerMinute: body.beatsPerMinute,
        energy: body.energy,
        danceability: body.danceability,
        loudnessIndB:body.loudnessIndB,
        liveness: body.liveness,
        valence: body.valence,
        length: body.length,
        acousticness: body.acousticness,
        speechiness: body.speechiness,
        popularity: body.popularity
    }
}
const AddOne = async (req,res)=>{
    try {
        //can be put in separe function createOne
        const song = await Song.create(getSongJson(req.body))
       
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

const DeleteOne = async (req,res) => { 
    try { 
        let result = await Song.findOneAndDelete({
            artistName: req.params.artist,
            trackName: req.params.track
        })
        let sendInfo = null
        if (!result) { 
            sendInfo = "Error: Song not found."
            return res.status(400).send(sendInfo)

        }
        sendInfo = "Song successfully deleted."
        res.status(200).send(sendInfo)
    }
    catch(error) { 
        res.status(500).send(error.message)
    }
}

const DeleteAll = async (req,res) => { 
    try { 
        
        let result = await Song.deleteMany({}) //no filter 
        let sendInfo = null
        if (!result) { 
            sendInfo = "Error: Songs not deleted."
            return res.status(400).send(sendInfo)

        }
        sendInfo = "Songs successfully deleted."
        res.status(200).send(sendInfo)
    }
    catch(error) { 
        res.status(500).send(error.message)
    }
}

const EditOne = async (req,res) => { 
    try { 
        let result = await Song.findOneAndUpdate({
            artistName: req.body.artist,
            trackName: req.body.track
        },getSongJson(req.body.song))
        let sendInfo = null
        if (!result) { 
            sendInfo = "Error: Song not found."
            return res.status(400).send(sendInfo)

        }
        sendInfo = "Song successfully updated."
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
        sendInfo = "Song successfully found"
        res.status(200).send(result)

    } catch (error) {
        res.status(500).send(error.message)
    }
}

const GetSongs = async (req,res) => { 
    try {
        let result = await Song.find().limit(req.params.limit)
        let sendInfo= ""

        if(!result){
            sendInfo = "Error: Songs not found"
            return res.status(400).send(sendInfo)
        }
        sendInfo = "Songs successfully found"
        res.status(200).send(result)

    } catch (error) {
        res.status(500).send(error.message)
    }
}

module.exports ={
    AddOne,
    AddMany,
    GetOne,
    DeleteOne,
    EditOne,
    DeleteAll,
    GetSongs
}

