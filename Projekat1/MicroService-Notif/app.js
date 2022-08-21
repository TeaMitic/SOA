const grpc = require("@grpc/grpc-js");
const protoLoader = require("@grpc/proto-loader");
const packageDef = protoLoader.loadSync("notification.proto", {});
const grpcObject = grpc.loadPackageDefinition(packageDef)
const notification = grpcObject.notif;

try {
    const server = new grpc.Server();
    server.addService(notification.Notification.service,
        {
            "sendNotif": sendNotif
        });
    server.bindAsync("0.0.0.0:8085", grpc.ServerCredentials.createInsecure(), () => {
        try {
            
            server.start();
            console.log('grpc server started')
        } catch (error) {
            throw error
        }
    
    })
    function sendNotif(call, callback){
        try {
            console.log(`New notification`);
            console.log("Data",call.request);
            callback(null, {res: "OK"})
        } catch (error) {
            throw error
        }
        
    }
}
catch (error) { 
    console.log(error);
}
