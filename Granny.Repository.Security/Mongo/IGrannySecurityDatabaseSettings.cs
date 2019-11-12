using System;
using System.Collections.Generic;
using System.Text;

namespace Granny.Repository.Security.Mongo
{
    public interface IGrannySecurityDatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
