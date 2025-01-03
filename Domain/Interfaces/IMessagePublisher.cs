namespace Domain.Interfaces
{
    public interface IMessagePublisher
    {
        Task publishAsync<T>(string queueName, T message);
    }
}
