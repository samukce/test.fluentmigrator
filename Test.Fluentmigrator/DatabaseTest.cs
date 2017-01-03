using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Test.Fluentmigrator.Configuration;
using Test.Fluentmigrator.Interfaces;
using Test.Fluentmigrator.ParallelsProvider;

namespace Test.Fluentmigrator {
    public class DatabaseTest {
        public IParallel Parallel = new SqlServerParallels();
        public IConfiguration Configuration = new SqlServerConfiguration();

        public static string ActualDatabaseName => ConfigKey("ActualDatabaseName");

        public static string ObjectiveDatabaseName => ConfigKey("ObjectiveDatabaseName");

        public static string ServerHostname => ConfigKey("ServerHostname");

        public static string User => ConfigKey("User");

        public static string Password => ConfigKey("Password");

        public static string Collation => ConfigKey("Collation");

        public DatabaseTest ActualDatabase(string schemaScript, string dataScript = null) {
            Configuration.LoadInit(GetActualDatabaseInfo(), schemaScript, dataScript);
            return this;
        }

        public DatabaseTest ObjectiveDatabase(string schemaScript) {
            Configuration.LoadInit(GetObjectiveDatabaseInfo(), schemaScript);
            return this;
        }

        public DatabaseTest RunMigration(int? specificVersion, string assemblyName = null) {
            new FluentMigrationRunner().Run(specificVersion, GetActualDatabaseInfo(), assemblyName);

            return this;
        }

        public void Compare(bool allErrors = true) {
            Parallel.Perform(GetActualDatabaseInfo(),GetObjectiveDatabaseInfo(), allErrors);
        }

        public void ExecuteCommandActualDatabase(Action<IDbCommand> command) {
            if (command == null) {
                throw new ArgumentNullException("command");
            }

            using (var connection = new SqlConnection(GetActualDatabaseInfo().GetConnectionString())) {
                connection.Open();
                using (var executeCommand = connection.CreateCommand()) {
                    command(executeCommand);
                }
            }
        }

        private static string ConfigKey(string key) {
            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("It's mandatory " + key + " in the config file of the test project.");

            return value;
        }

        private static DatabaseInfo GetObjectiveDatabaseInfo() {
            return new DatabaseInfo {
                DatabaseName = ObjectiveDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            };
        }

        private static DatabaseInfo GetActualDatabaseInfo() {
            return new DatabaseInfo {
                DatabaseName = ActualDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            };
        }
    }
}
