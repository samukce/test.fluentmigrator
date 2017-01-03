using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;

namespace Test.Fluentmigrator {
    public class FluentMigrationRunner {

        public void Run(int? version, DatabaseInfo databaseInfo, string assemblyName = null, IEnumerable<string> tags = null) {
            var announcer = new TextWriterAnnouncer(OutputWriter) {
                ShowSql = true,
                ShowElapsedTime = true
            };

            var migrationProcessorOptions = new MigrationOptions();

            var processor = new SqlServer2008ProcessorFactory().Create(databaseInfo.GetConnectionString(), announcer, migrationProcessorOptions);

            var assembly = string.IsNullOrWhiteSpace(assemblyName)
                             ? Assembly.GetExecutingAssembly()
                             : Assembly.Load(assemblyName);

            var runnerContext = new RunnerContext(announcer) { Tags = tags };

            var runner = new MigrationRunner(assembly, runnerContext, processor);

            if (version == null) {
                runner.MigrateUp(true);

                return;
            }

            var actualVersion = runner.VersionLoader.VersionInfo.Latest();

            if (actualVersion == version) return;

            if (actualVersion < version) {
                runner.MigrateUp(version.Value, true);
            } else {
                runner.MigrateDown(version.Value, true);
            }
        }

        private void OutputWriter(string message) {
            Debug.WriteLine(message);
        }

        private class MigrationOptions : IMigrationProcessorOptions {
            public bool PreviewOnly { get { return false; } }
            public int Timeout { get { return int.MaxValue; } }
            public string ProviderSwitches { get; private set; }
        }
    }
}
