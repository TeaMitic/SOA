module.exports={
    get:{
        description: "Gets limited number of songs",
        operationId: "getSongs",
        parameters: [
            {
                name: "limit",
                in: "path",
                schema: {
                    $ref: "#/api-doc/schemas/Limit",
                },
                required: true,
                description: "Limit on number of songs being returned",
            }
        ],
        responses: {
            200: {
                description: "Songs are obtained",
                content:{
                    "application/json":{
                        schema:{
                            $ref: "#/api-doc/schemas/Song",
                        },
                    },
                },
            },
            400: {
                description: "Songs not found"
            },
            500: {
                description: "Server error"
            }
        }
    }
}