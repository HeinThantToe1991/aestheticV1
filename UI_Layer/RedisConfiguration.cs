namespace UI_Layer
{
    public class RedisConfiguration
    {
        public RedisConfiguration(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }
    }
}
