using Application.Notifications.Commands.Create;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Infrastructure.Messaging
{
    public class RabbitMqConsumer : IMessageConsumer
    {
        private readonly RabitmqConnectionFactory _connectionFactory;
        private readonly IMediator _mediator;
        public RabbitMqConsumer(RabitmqConnectionFactory rabitmqConnectionFactory, IMediator mediator)
        {
            _connectionFactory = rabitmqConnectionFactory;
            _mediator = mediator;
        }
        public async Task StartConsumerAsync(string queueName, CancellationToken cancellationToken)
        {
            try
            {
                using var channel = _connectionFactory.CreateChannel();
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    var body = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var notification = JsonSerializer.Deserialize<Notification>(body);
                    if (notification != null)
                    {
                        // await _mediator.Send(new SendNotificationCommand(notification));
                        await _mediator.Send(new SendNotificationCommand(notification));
                    }

                    channel.BasicAck(ea.DeliveryTag, false);
                };

                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(1000, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
