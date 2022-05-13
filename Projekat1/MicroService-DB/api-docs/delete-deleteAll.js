module.exports={
    delete:{
        description: "Deleting all songs",
        operationId: "deleteAll",
        parameters:[],
        responses:{
            200: {
                description: "Songs deleted successfully", 
            },              
            400: {
                description: "Songs not deleted", 
            },
            500: {
                description: "Server error", 
            },
        }
    }
}