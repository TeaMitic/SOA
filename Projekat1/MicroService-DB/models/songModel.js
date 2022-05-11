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
        type: Number,
        required: true
    },
    energy:{
        type: Number,
        required: true
    },
    danceability:{
        type: Number,
        required: true
    },
    loudnessIndB:{
        type: Number,
        required: true
    },
    liveness:{
        type: Number,
        required: true
    },
    valence:{
        type: Number,
        required: true
    },
    length:{
        type: Number,
        required: true
    },
    acousticness:{
        type: Number,
        required: true
    },
    speechiness:{
        type: Number,
        required: true
    },
    popularity:{
        type: Number,
        required: true
    } 
})

songSchema.index({
    trackName: 1,
    artistName: 1
},{
    unique: true,
    name: "song-uniqueness"
})

const Song = mongoose.model('Song', songSchema)
module.exports = Song
