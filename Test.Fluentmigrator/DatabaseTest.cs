using System;
using System.Collections.Generic;
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

        public DatabaseTest RunMigration(long? version, string assemblyName = null, IEnumerable<string> tags = null) {
            new FluentMigrationRunner().Run(version, GetActualDatabaseInfo(), assemblyName, tags);

            return this;
        }

        public DatabaseTest RevertMigrationBefore(long version, string assemblyName, IEnumerable<string> tags = null) {
            new FluentMigrationRunner().RevertMigrationBefore(version, GetActualDatabaseInfo(), assemblyName, tags);
            return this;
        }

        /// <summary>
        /// Compare the Actual with Objective database. If found any difference will be throw exception with details these differences.
        /// </summary>
        /// <param name="allErrors">if false on the first difference between databases will throw exception.</param>
        public void Compare(bool allErrors = true) {
            Parallel.Perform(GetActualDatabaseInfo(), GetObjectiveDatabaseInfo(), allErrors);
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
