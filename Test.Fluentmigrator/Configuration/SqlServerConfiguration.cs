using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Test.Fluentmigrator.Interfaces;

namespace Test.Fluentmigrator.Configuration {
    public class SqlServerConfiguration : IConfiguration {
        private Server Server { get; set; }

        public void LoadInit(DatabaseInfo databaseInfo, string schemaScript, string dataScript = null) {
            Server = InitializeServer(databaseInfo);

            if (Server.Databases.Contains(databaseInfo.DatabaseName)) {
                Server.KillDatabase(databaseInfo.DatabaseName);
            }

            var database = new Database(Server, databaseInfo.DatabaseName) {
                Collation = databaseInfo.Collation
            };

            Server.Databases.Add(database);
            database.Create();

            database.ExecuteNonQuery(schemaScript);

            if (!string.IsNullOrWhiteSpace(dataScript)) {
                database.ExecuteNonQuery(dataScript);
            }
        }

        public static Server InitializeServer(DatabaseInfo databaseInfo) {
            var serverConnection = new ServerConnection(databaseInfo.ServerHostname, 
                                                        databaseInfo.User, 
                                                        databaseInfo.Password);

            serverConnection.Connect();
            serverConnection.Disconnect();

            return new Server(serverConnection);
        }
    }
}
