module.exports={
    post:{
        description: "Add song to mongoDB",
        operationId: "addOne",
        parameters: [],
        requestBody: {
            content:{
                "application/json": {
                    schema:{
                        $ref: "#/api-doc/schemas/Song"
                    },
                },
            },
        },
        responses: {
            200:{
                description: "Song added successfully",
            },
            400:{
                description: "Song not added. Song already exists or fields are not defined right."
            },
            500: {
                description: "Server error"
            }
        }
    }
}