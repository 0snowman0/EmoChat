﻿namespace Analys.api.config.settings
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string InstanceName { get; set; } = null!;
        public int DefaultCacheTimeInMinutes { get; set; }
    }
}
