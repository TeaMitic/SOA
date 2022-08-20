const grpc = require("@grpc/grpc-js");
const protoLoader = require("@grpc/proto-loader");
const packageDef = protoLoader.loadSync("notification.proto", {});
const grpcObject = grpc.loadPackageDefinition(packageDef)
const notification = grpcObject.notif;


const server = new grpc.Server();
server.addService(notification.Notification.service,
    {
        "sendNotif": sendNotif
    });
server.bindAsync("0.0.0.0:8085", grpc.ServerCredentials.createInsecure(), () => {
    server.start();
    console.log('grpc server started')

})
function sendNotif(call, callback){
    // console.log("Call:",call)
    // callback(null,  {res:  call.request.notif})
    console.log(`New notification`);
    console.log("Data",call.request);
}
