module.exports={
    post:{
        description: "Add songs to mongoDB",
        operationId: "addMany",
        parameters: [],
        requestBody: {
            //ovde treba lista da ide, nzm kako
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
                description: "Songs added successfully",
            },
            400:{
                description: "Songs not added. Song already exists or fields are not defined right."
            },
            500: {
                description: "Server error"
            }
        }
    }
}