const mongoose= require('mongoose')
const Song = require('../models/songModel')

//functions

const AddOne = async (req,res)=>{
    try {
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

const AddMany = async (songs)=>{
    await Song.insertMany(songs).then(()=> {
        let sendInfo = "Songs successfully added"
        res.status(200).send(sendInfo)
    })
    .catch(function (err) {
        response.status(500).send(err.message);
    });
    // try{
        // songs.forEach(element => {
        //     const song = await Song.create({
        //         trackName: element.trackName,
        //         artistName: element.artistName,
        //         genre: element.genre,
        //         beatsPerMinute: element.beatsPerMinute,
        //         energy: element.energy,
        //         danceability: element.danceability,
        //         loudnesIndB: element.loudnesIndB,
        //         liveness: element.liveness,
        //         valence: element.artvalenceistName,
        //         lenght: element.lenght,
        //         acousticness: element.acousticness,
        //         speechiness: element.speechiness,
        //         popularity: element.popularity
        //     })
            
        // });
        // let sendInfo = "Songs successfully added"
        // res.status(200).send(sendInfo)
        
//     } catch (error) {
//         res.status(500).send(error.message)
//     }
}

module.exports ={
    AddOne,
    AddMany
}