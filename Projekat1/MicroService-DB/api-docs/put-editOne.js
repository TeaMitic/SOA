module.exports={
    put:{
        description:"Edit song",
        operationId: "editOne",
        parameters: [],
        requestBody: {
            content:{
                "application/json": {
                    schema:{
                        $ref: "#/api-doc/schemas/Song"
                    },
                },
                "application/json": {
                    schema:{
                        $ref: "#/api-doc/schemas/TrackName"
                    },
                },
                "application/json": {
                    schema:{
                        $ref: "#/api-doc/schemas/ArtistName"
                    },
                }
            },
        },
        responses: {
            200: {
                description: "Song updated successfully"
            },
            400:{
                description: "Song not found or fields are not defined right"
            },
            500:{
                description: "Server error"
            }
        }
    }
}