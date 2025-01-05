namespace Domain.Interfaces
{
    public interface IMessageConsumer
    {
        Task StartConsumerAsync(string queueName, CancellationToken cancellationToken);
    }
}
