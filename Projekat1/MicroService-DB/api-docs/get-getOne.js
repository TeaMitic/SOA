module.exports={
    get:{
        description: "Get a song by track name and artist name",
        operationId: "getOne",
        parameters: [
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
        responses: {
            200: {
                description: "Song is obtained",
                content:{
                    "application/json":{
                        schema:{
                            $ref: "#/api-doc/schemas/Song",
                        },
                    },
                },
            },
            400: {
                description: "Song not found"
            },
            500: {
                description: "Server error"
            }
        }
    }
}