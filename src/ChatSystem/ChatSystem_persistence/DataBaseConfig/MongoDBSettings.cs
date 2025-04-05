namespace ChatSystem_persistence.DataBaseConfig
{
    class MongoDBSettings
    {
        public MongoDatabaseSettings WriteDatabase { get; set; }
        public MongoDatabaseSettings ReadDatabase { get; set; }
    }
}
