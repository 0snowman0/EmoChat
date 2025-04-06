namespace ChatSystem_persistence.DataBaseConfig
{
    public class MongoDBSettings
    {
        public MongoDatabaseSettings WriteDatabase { get; set; }
        public MongoDatabaseSettings ReadDatabase { get; set; }
    }
}
