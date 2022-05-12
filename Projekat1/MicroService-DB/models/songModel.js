const mongoose = require('mongoose')
const Schema = mongoose.Schema

/**
 * @swagger
 * components:
 *  schemas:
 *      Song:
 *          type: object
 *          required:
 *              - trackName
 *              - artistName
 *              - genre
 *              - beatsPerMinute
 *              - energy
 *              - danceability
 *              - loudnessIndB
 *              - liveness
 *              - valence
 *              - length
 *              - acousticness
 *              - speechiness
 *              - popularity
 *          properties:
 *              _id:
 *                  type: string
 *                  description: The Auto-generated id of a song
 *              trackName:
 *                  type: string
 *                  description: Name of the Track
 *              artistName:
 *                  type: string
 *                  description: Name of the Artist
 *              genre:
 *                  type: string
 *                  description: The Genre of the Track
 *              beatsPerMinute:
 *                  type: integer
 *                  description: The tempo of the song.
 *              energy:
 *                  type: integer
 *                  description: The energy of a song - the higher the value, the more energtic song
 *              danceability:
 *                  type: integer
 *                  description: The higher the value, the easier it is to dance to this song.
 *              loudnessIndB:
 *                  type: integer
 *                  description: The higher the value, the louder the song.
 *              liveness:
 *                  type: integer
 *                  description: The higher the value, the more likely the song is a live recording.
 *              valence:
 *                  type: integer
 *                  description: The higher the value, the more positive mood for the song.
 *              length:
 *                  type: integer
 *                  description: The duration of the song in seconds.
 *              acousticness:
 *                  type: integer
 *                  description: The higher the value the more acoustic the song is.
 *              speechiness:
 *                  type: integer
 *                  description: The higher the value the more spoken word the song contains.
 *              popularity:
 *                  type: integer
 *                  description: The higher the value the more popular the song is.
 *          example:
 *              _id: 627d6F67366077a9e1651der
 *              trackNane: "Lose yourself"
 *              artistName: "Eninen"
 *              genre: "rap"
 *              beatsPertinute: 25
 *              energy: 30
 *              danceability: 15
 *              loudnessInde: 40
 *              liveness: 10
 *              valence: 50
 *              length: 60
 *              acousticness: 1e
 *              speechiness: 80
 *              popularity: 100
 * 
 */

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
