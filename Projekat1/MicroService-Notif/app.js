const grpc = require("grpc");
const protoLoader = require("@grpc/proto-loader");
const packageDef = protoLoader.loadSync("notification.proto", {});
const grpcObject = grpc.loadPackageDefinition(packageDef)
const notification = grpcObject.notification;

const server = new grpc.Server();
server.bind("notification:8085", grpc.ServerCredentials.createInsecure())
server.addService(notification.Notification.service,
    {
        "sendNotif": sendNotif
    });
server.start();
function sendNotif(call, callback){
    console.log(call)
}