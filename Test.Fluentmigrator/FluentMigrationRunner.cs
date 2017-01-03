using System.Diagnostics;
using System.Reflection;
using FluentMigrator;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Announcers;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Processors.SqlServer;

namespace Test.Fluentmigrator {
    public class FluentMigrationRunner {

        public void Run(int? versao, DatabaseInfo databaseInfo, string assemblyName = null) {
            var connectionString = $@"Server={databaseInfo.ServerHostname};Database={databaseInfo.DatabaseName};User ID={databaseInfo.User};Password={databaseInfo.Password};";
            
            var announcer = new TextWriterAnnouncer(OutputWriter) {
                ShowSql = true,
                ShowElapsedTime = true
            };

            var processor = new SqlServer2008ProcessorFactory().Create(connectionString, announcer, new MigrationOptions());

            var assembly = string.IsNullOrWhiteSpace(assemblyName)
                ? Assembly.GetExecutingAssembly()
                : Assembly.Load(assemblyName);

            var runner = new MigrationRunner(assembly, new RunnerContext(announcer), processor);

            if (versao == null) {
                runner.MigrateUp(true);

                return;
            }

            var versaoAtual = runner.VersionLoader.VersionInfo.Latest();

            if (versaoAtual == versao) return;

            if (versaoAtual < versao) {
                runner.MigrateUp(versao.Value, true);
            } else {
                runner.MigrateDown(versao.Value, true);
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
