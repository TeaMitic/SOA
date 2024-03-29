using MQTTnet;
using MQTTnet.Client;

namespace MicroService_Gateway.MQTT
{
    public interface IMQTTService 
    {
        Task PublishEvent(string topicName, string payload);
    }
    public class MQTTService : IMQTTService, IDisposable 
    {
        private IMqttClient _mqttClient;
       
        public MQTTService() 
        {
            var mqttFactory = new MqttFactory();
            _mqttClient = mqttFactory.CreateMqttClient();
        }
        public void Dispose()
        {
            _mqttClient.Dispose();
        }

        public async Task PublishEvent(string topicName, string payload)
        {
            
            if (!_mqttClient.IsConnected)
				await Connect();
            var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topicName)
                    .WithPayload(payload)
                    .Build();

            await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
        }

        public async Task Connect() 
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("mqtt",1883)
                    .Build();
            await _mqttClient.ConnectAsync(mqttClientOptions);

        }
    }
}