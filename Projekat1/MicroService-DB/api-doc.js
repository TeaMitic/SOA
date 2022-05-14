const options ={
   definition:{
       openapi: "3.0.1",
       info:{
           title: "Songs persistence API",
           version: "1.0.0",
           description: "API for persisting songs into mongoDB",
           //termsOfService: nesto
           contact:{
               name: "Tea Mitic & Dimitrije Mitic",
               url: "http://www.example.com/support"
           },
       },
       components:{
           schemas: {
               //song model
               Song:{
                   type: "object",
                   required: ['trackName', 'artistName', 'genre', 'beatsPerMinute', 'energy', 'danceability', 'loudnessIndB', 'liveness', 'valence', 'length', 'acousticness', 'speechiness', 'popularity'],
                   properties: {
                        _id:{
                            type: "string",
                            description: "The Auto-generated id of a song",
                            example: "627d6F67366077a9e1651der"
                        },
                        trackName:{
                            type: "string",
                            description: "Name of the Track",
                            example: "Lose yourself"
                        },
                        artistName:{
                            type: "string",
                            description: "Name of the Artist",
                            example: "Eminem"
                        },
                        genre:{
                            type: "string",
                            description: "The Genre of the Track",
                            example: "rap"
                        },
                        beatsPerMinute:{
                            type: "integer",
                            description: "The tempo of the song.",
                            example: "25"
                        },
                        energy:{
                            type: "integer",
                            description: "The energy of a song - the higher the value, the more energtic song",
                            example: "30"
                        },
                        danceability:{
                            type: "integer",
                            description: "The higher the value, the easier it is to dance to this song.",
                            example: "15"
                        },
                        loudnessIndB: {
                            type: "integer",
                            description: "The higher the value, the louder the song.",
                            example: "40"
                        },
                        liveness:{
                            type: "integer",
                            description: "The higher the value, the more likely the song is a live recording.",
                            example: "10"
                        },
                        valence:{
                            type: "integer",
                            description: "The higher the value, the more positive mood for the song.",
                            example: "50"
                        },
                        length:{
                            type: "integer",
                            description: "The duration of the song in seconds.",
                            example: "60"
                        },
                        acousticness:{
                            type: "integer",
                            description: "The higher the value the more acoustic the song is.",
                            example: "10"
                        },
                        speechiness:{
                            type: "integer",
                            description: "The higher the value the more spoken word the song contains.",
                            example: "80"
                        },
                        popularity:{
                            type: "integer",
                            description: "The higher the value the more popular the song is.",
                            example: "100"
                        },
                   }
               },
               //trackName of a song
               TrackName:{
                    type: "string",
                    description: "Name of a song"
               },
               //artistName of a song
               ArtistName:{
                    type: "string",
                    description: "Artist name of a song"
               }
           }
       },
       servers: [
           {
               url: "http://localhost:5000",
               description: "Songs API documentation"
           },
       ],
       
    },
       apis: ['./routes/*.js'],
};

module.exports = options