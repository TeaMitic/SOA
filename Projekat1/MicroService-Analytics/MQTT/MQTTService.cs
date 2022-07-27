using System.Text.Json;
using MicroService_Analytics.DTO;
using MQTTnet;
using MQTTnet.Client;

namespace MicroService_Analytics.MQTT
{
    public interface IMQTTService 
    {
        Task PublishEvent(string topicName, Agriculture agr);
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
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
		{
			PropertyNamingPolicy = JsonNamingPolicy.CamelCase
		};
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

        public async Task PublishEvent(string topicName, Agriculture agr)
		{
			if (!_mqttClient.IsConnected)
				await Connect();
			var applicationMessage = new MqttApplicationMessageBuilder()
					.WithTopic(topicName)
					.WithPayload(JsonSerializer.Serialize(agr, _jsonSerializerOptions))
					.Build();

			await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
		}

        public async Task SubscribeTopic(string topicName) 
        {
            var mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topicName); })
                .Build();

            var response = await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }

     

       
    }
}