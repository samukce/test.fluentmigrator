using System;
using System.Configuration;
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

        public DatabaseTest ActualDatabase(string schemaScript) {
            Configuration.LoadInit(new DatabaseInfo {
                DatabaseName = ActualDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            }, schemaScript);
            return this;
        }

        public DatabaseTest ObjectiveDatabase(string schemaScript) {
            Configuration.LoadInit(new DatabaseInfo {
                DatabaseName = ObjectiveDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            }, schemaScript);
            return this;
        }

        public DatabaseTest RunMigration() {
            return this;
        }

        public void Compare(bool allErrors = true) {
            Parallel.Perform(new DatabaseInfo {
                DatabaseName = ActualDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            }, new DatabaseInfo {
                DatabaseName = ObjectiveDatabaseName,
                ServerHostname = ServerHostname,
                User = User,
                Password = Password,
                Collation = Collation
            }, allErrors);
        }

        private static string ConfigKey(string key) {
            var value = ConfigurationManager.AppSettings[key];

            if (string.IsNullOrWhiteSpace(value))
                throw new Exception("It's mandatory " + key + " in the config file of the test project.");

            return value;
        }
    }
}
