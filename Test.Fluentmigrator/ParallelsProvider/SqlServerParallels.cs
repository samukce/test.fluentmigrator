using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Test.Fluentmigrator.Configuration;
using Test.Fluentmigrator.Exceptions;
using Test.Fluentmigrator.Interfaces;

namespace Test.Fluentmigrator.ParallelsProvider {
    public class SqlServerParallels : IParallel {
        private List<MigrationFailedException> migrationsFailedException = new List<MigrationFailedException>();
        private bool allErrors;

        public void Perform(DatabaseInfo actual, DatabaseInfo objective, bool allErrors) {
            this.allErrors = allErrors;

            Perform(SqlServerConfiguration.InitializeServer(actual),
                    actual.DatabaseName,
                    objective.DatabaseName);
        }

        private void Perform(Server serverActual,
                             string databaseNameActual,
                             string databaseNameObjective) {
            migrationsFailedException = new List<MigrationFailedException>();

            var databaseActual = serverActual.Databases[databaseNameActual];
            var databaseObjective = serverActual.Databases[databaseNameObjective];

            Compare(databaseNameActual, databaseObjective, databaseActual);
            Compare(databaseNameObjective, databaseActual, databaseObjective);

            ThrowExceptions();
        }

        private void Compare(string databaseNameToCompare, Database databaseObjective, Database databaseActual) {
            foreach (Table tableModel in databaseObjective.Tables) {
                if (!databaseActual.Tables.Contains(tableModel.Name, tableModel.Schema)) {
                    var tableFullName = $"{tableModel.Schema}.{tableModel.Name}";
                    RegistrarException(new TableNotFoundException(databaseNameToCompare, tableFullName));
                    continue;
                }

                var tabelaToCompare = databaseActual.Tables[tableModel.Name, tableModel.Schema];

                //Tables(databaseNameToCompare, tabelaToCompare, tableModel);
            }
        }

        private void RegistrarException(MigrationFailedException exception) {
            if (!allErrors) {
                throw exception;
            }

            migrationsFailedException.Add(exception);
        }

        private void ThrowExceptions() {
            if (migrationsFailedException.Count == 0) return;

            var stringBuilder = new StringBuilder();
            foreach (var migrationFailedException in migrationsFailedException) {
                stringBuilder.AppendLine(migrationFailedException.Message);
            }

            throw new TestFluentMigratorException(stringBuilder.ToString());
        }
    }
}
