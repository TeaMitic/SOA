using System.Text.Json;
using MicroService_Analytics.DTO;
using MQTTnet;
using MQTTnet.Client;

namespace MicroService_Analytics.MQTT
{
    public interface IMQTTService 
    {
        Task PublishEvent(string topicName, string payload);
    }
    public class MQTTService : IMQTTService, IDisposable 
    {
        private IMqttClient _mqttClient;
        private MqttFactory _mqttFactory;
        public IMqttClient Listener 
        { 
            get 
            {
                return _mqttClient;
            }
        }
       
        public MQTTService() 
        {
            _mqttFactory = new MqttFactory();
            _mqttClient = _mqttFactory.CreateMqttClient();
        }
        public void Dispose()
        {
            _mqttClient.Dispose();
        }

        public async Task Connect() 
        {
            var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer("mqtt",1883)
                    .Build();
            await _mqttClient.ConnectAsync(mqttClientOptions);

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

        public async Task SubscribeTopic(string topicName) 
        {
            if (!_mqttClient.IsConnected)
				await Connect();
            var mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topicName); })
                .Build();

            var response = await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

     

       
    }
}