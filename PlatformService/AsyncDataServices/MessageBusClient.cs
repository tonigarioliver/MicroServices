using System.Text;
using System.Text.Json;
using PlatformService.Dtos;
using RabbitMQ.Client;

namespace PlatformService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private IConfiguration _configuration;

        public MessageBusClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var factroy = new ConnectionFactory(){HostName = _configuration["RabbitMQHost"], 
            Port=int.Parse(_configuration["RabbitMQPort"])};
            try
            {
               _connection = factroy.CreateConnection(); 
               _channel = _connection.CreateModel();
               _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
               _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
                Console.WriteLine(" -->Connection to MessageBus");
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not connect to RabbitMQ: {e.Message}");
            }
        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine("--> Shutdown RabbitMQ");
        }
        public void PublishNewPlatform(PlatformPublishedDto platformPublishedDto)
        {
            var message = JsonSerializer.Serialize(platformPublishedDto);
            if(_connection.IsOpen)
            {
                Console.WriteLine("--> RabbitMQ Connection Open, sending message..");
                // To do send the message
                SendMessage(message);

            }
            Console.WriteLine("--> RabbitMQ Connection Closed, not sending message");
        }
        private void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange:"trigger",
                                routingKey: "",
                                basicProperties: null,
                                body: body);
            Console.WriteLine($"We have sent: {message}");
        }

        public void Dispose()
        {
            Console.WriteLine("MessageBus Disposed");
            if(_channel.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }
    }
}