

using Domain.Interfaces;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging
{
    public class RabbitMqNotificationService : IMessagePublisher
    {
        private readonly RabitmqConnectionFactory _connectionFactory;
        public RabbitMqNotificationService(RabitmqConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task publishAsync<T>(string queueName, T message)
        {
            using var channel = _connectionFactory.Connection.CreateModel();
            channel.QueueDeclare(queue: queueName, // Ensure this matches the routing key
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

            var json = JsonSerializer.Serialize(message);
            var messageBody = Encoding.UTF8.GetBytes(json);

            // Publish to the correct queue
            channel.BasicPublish(exchange: "", routingKey: queueName, mandatory: false, basicProperties: null,
                body: messageBody);
            await Task.CompletedTask;
        }
    }
}
