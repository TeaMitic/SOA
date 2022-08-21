using Grpc.Net.Client;
using GrpcClient;

namespace MicroService_Analytics.gRPC 
{
    public static class gRPCWrapper 
    {
        private static GrpcClient.Notification.NotificationClient? _grpcClient = null;

        private static GrpcChannel configureGRPC(){ 
            // grpc deo
            var httpHandler = new HttpClientHandler();
            //Return true to allow certificates that are untrusted/invalid
            httpHandler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
            var channel = GrpcChannel.ForAddress("http://service-notif:8085",new GrpcChannelOptions {HttpHandler = httpHandler});
            // CancellationToken cancellationToken = default(CancellationToken)
            return channel;
    }

        public static GrpcClient.Notification.NotificationClient GRPCClient {
            get { 
                if (_grpcClient == null) { 
                    _grpcClient = new GrpcClient.Notification.NotificationClient(configureGRPC());
                }
                return _grpcClient;
            }
        }


        
    }
}

