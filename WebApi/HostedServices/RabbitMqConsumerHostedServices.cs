using Domain.Interfaces;

namespace WebApi.HostedServices
{
    public class RabbitMqConsumerHostedServices:BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly string _queueName;
        public RabbitMqConsumerHostedServices(IServiceScopeFactory serviceScopeFactory, string queueName)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _queueName = queueName;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var consumer = scope.ServiceProvider.GetRequiredService<IMessageConsumer>();
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Consume messages continuously
                    await consumer.StartConsumerAsync(_queueName, stoppingToken);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in {_queueName}: {ex.Message}");
                    await Task.Delay(5000, stoppingToken); // Retry after delay
                }
            }
        }
    }
}
