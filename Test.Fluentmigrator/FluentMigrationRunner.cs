using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;
using Test.Fluentmigrator.Exceptions;

namespace Test.Fluentmigrator {
    public class FluentMigrationRunner {

        public void Run(long? version, DatabaseInfo databaseInfo, string assemblyName = null, IEnumerable<string> tags = null) {
            var runner = BuildMigrationRunner(databaseInfo, assemblyName, tags);

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

        public void RevertMigrationBefore(long version, DatabaseInfo databaseInfo, string assemblyName = null, IEnumerable<string> tags = null) {
            var runner = BuildMigrationRunner(databaseInfo, assemblyName, tags);

            var appliedMigrations = runner.VersionLoader.VersionInfo.AppliedMigrations().OrderByDescending(v => v);

            if (!appliedMigrations.Any()) {
                throw new MigrationsNotFoundException();
            }

            if (appliedMigrations.Count() == 1 && version == appliedMigrations.First()) {
                runner.MigrateDown(0, true);
                return;
            }

            foreach (var appliedMigration in appliedMigrations) {
                if (appliedMigration >= version)
                    continue;

                runner.MigrateDown(appliedMigration, true);
                break;
            }
        }

        private MigrationRunner BuildMigrationRunner(DatabaseInfo databaseInfo, string assemblyName, IEnumerable<string> tags) {
            var announcer = new TextWriterAnnouncer(OutputWriter) {
                ShowSql = true,
                ShowElapsedTime = true
            };

            var migrationProcessorOptions = new MigrationOptions();

            var processor = new SqlServer2008ProcessorFactory().Create(databaseInfo.GetConnectionString(), announcer,
                migrationProcessorOptions);

            var assembly = string.IsNullOrWhiteSpace(assemblyName)
                ? Assembly.GetExecutingAssembly()
                : Assembly.Load(assemblyName);

            var runnerContext = new RunnerContext(announcer) { Tags = tags };

            return new MigrationRunner(assembly, runnerContext, processor);
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
