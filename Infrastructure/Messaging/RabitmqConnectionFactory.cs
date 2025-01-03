using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;

namespace Infrastructure.Messaging
{
    public class RabitmqConnectionFactory : IDisposable
    {
        private IConnection _connection;
        public IConnection Connection => _connection;
        private readonly IConfiguration _configuration;

        public RabitmqConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
            initializeConnection();
        }
        private void initializeConnection()
        {
            try
            {
                var factory = new ConnectionFactory
                {
                    HostName = _configuration["RabbitMQ:Host"],
                    Port = int.Parse(_configuration["RabbitMQ:Port"]),
                    UserName = _configuration["RabbitMQ:Username"],
                    Password = _configuration["RabbitMQ:Password"]
                };
                _connection =factory.CreateConnection();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
    }
}
