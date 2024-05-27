using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MotorcyleRental.Messaging
{
    public class MessageMQService : IMessageMQService
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public MessageMQService(IConfiguration configuration)
        {
            _hostname = configuration["RabbitMQ:Hostname"];
            _queueName = configuration["RabbitMQ:QueueName"];

            var factory = new ConnectionFactory() { HostName = _hostname };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        public void SendMessage(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);
        }

        public IEnumerable<string> ReceiveMessages()
        {
            var messages = new List<string>();
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                messages.Add(message);
            };
            _channel.BasicConsume(queue: _queueName,
                                 autoAck: true,
                                 consumer: consumer);

            return messages;
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}
