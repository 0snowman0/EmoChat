﻿namespace ChatSystem_persistence.Event
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}
