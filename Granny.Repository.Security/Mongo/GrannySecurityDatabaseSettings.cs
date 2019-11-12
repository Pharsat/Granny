namespace Granny.Repository.Security.Mongo
{
    public class GrannySecurityDatabaseSettings : IGrannySecurityDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
