const mongoose = require('mongoose')
const Schema = mongoose.Schema

const songSchema = new Schema({
    trackName:{
        type: String,
        required: true
    },
    artistName:{
        type: String,
        required: true
    },
    genre:{
        type: String,
        required: true
    },
    beatsPerMinute:{
        type: String,
        required: true
    },
    energy:{
        type: String,
        required: true
    },
    danceability:{
        type: String,
        required: true
    },
    loudnesIndB:{
        type: String,
        required: true
    },
    liveness:{
        type: String,
        required: true
    },
    valence:{
        type: String,
        required: true
    },
    lenght:{
        type: String,
        required: true
    },
    acousticness:{
        type: String,
        required: true
    },
    speechiness:{
        type: String,
        required: true
    },
    popularity:{
        type: String,
        required: true
    } 
})

const Song = mongoose.model('Song', songSchema)

module.exports = Song
