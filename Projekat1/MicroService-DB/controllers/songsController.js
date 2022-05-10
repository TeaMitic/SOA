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
        loudnesIndB:body.loudnesIndB,
        liveness: body.liveness,
        valence: body.valence,
        lenght: body.lenght,
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
        let sendInfo = null
        if (!result) { 
            sendInfo = "Error while adding songs."
            res.status(400).send(sendInfo)

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

const EditOne = async (req,res) => { 
    try { 
        let result = await Song.findOneAndUpdate({
            artistName: req.body.searchFilter.artistName,
            trackName: req.body.searchFilter.trackName
        },getSongJson(req.body.updateObject))
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

module.exports ={
    AddOne,
    AddMany,
    DeleteOne,
    EditOne
}

