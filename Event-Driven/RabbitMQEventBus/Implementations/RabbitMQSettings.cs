namespace RabbitMQEventBus.Implementations
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; } = 3;
        public int PrefetchCount { get; set; } = 10;
    }
}
