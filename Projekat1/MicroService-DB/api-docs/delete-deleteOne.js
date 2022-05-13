module.exports={
    delete:{
        description: "Deleting a song",
        operationId: "deleteOne",
        parameters:[
            {
                name: "trackName",
                in: "path",
                schema: {
                    $ref: "#/api-doc/schemas/TrackName",
                },
                required: true,
                description: "Song name",
            },
            {
                name: "artistName",
                in: "path",
                schema: {
                    $ref: "#/api-doc/schemas/ArtistName"
                },
                required: true,
                description: "Artist name",
            }
        ],
        responses:{
            200: {
                description: "Song deleted successfully", 
            },              
            400: {
                description: "Song not found", 
            },
            500: {
                description: "Server error", 
            },
        }
    }
}