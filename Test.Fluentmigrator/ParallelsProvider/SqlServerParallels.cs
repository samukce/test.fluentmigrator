using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Management.Smo;
using Test.Fluentmigrator.Configuration;
using Test.Fluentmigrator.Exceptions;
using Test.Fluentmigrator.Interfaces;

namespace Test.Fluentmigrator.ParallelsProvider {
    public class SqlServerParallels : IParallel {
        private const string TableNameFormat = "{0}.{1}";
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
                    AddException(new TableNotFoundException(tableFullName, databaseNameToCompare));
                    continue;
                }

                var tabelaToCompare = databaseActual.Tables[tableModel.Name, tableModel.Schema];

                Tables(databaseNameToCompare, tabelaToCompare, tableModel);
            }
        }

        private void Tables(string databaseName, Table tableA, Table tableB) {
            var nomeTabelaA = string.Format(TableNameFormat, tableA.Schema, tableA.Name);
            var nomeTabelaB = string.Format(TableNameFormat, tableB.Schema, tableB.Name);

            foreach (Column columnB in tableB.Columns) {
                if (!tableA.Columns.Contains(columnB.Name)) {
                    AddException(new ColumnNotFoundException(databaseName, nomeTabelaB, columnB.Name));
                    continue;
                }

                var columnA = tableA.Columns[columnB.Name];

                if (columnB.Nullable != columnA.Nullable)
                    AddException(new NullablePropertyDifferentException(databaseName, nomeTabelaB, columnB.Name, columnB.Nullable));

                if (columnB.Identity != columnA.Identity)
                    AddException(new ColumnIdentityPropertyNotFoundException(databaseName, nomeTabelaA, columnB.Name, columnB.Identity));

                if (columnB.DataType.SqlDataType != columnA.DataType.SqlDataType || columnB.DataType.Name != columnA.DataType.Name)
                    AddException(new ColumnDifferentTypeException(databaseName, nomeTabelaB, columnB.Name, columnB.DataType.SqlDataType.ToString(), columnA.DataType.SqlDataType.ToString()));

                if (columnB.DataType.MaximumLength != columnA.DataType.MaximumLength)
                    AddException(new ColumnDifferentSizeException(databaseName, nomeTabelaB, columnB.Name, columnB.DataType.MaximumLength, columnA.DataType.MaximumLength));

                if (columnB.DataType.NumericPrecision != columnA.DataType.NumericPrecision)
                    AddException(new ColumnDifferentPrecisionException(databaseName, nomeTabelaB, columnB.Name, columnB.DataType.NumericPrecision, columnA.DataType.NumericPrecision));

                if (columnB.DataType.NumericScale != columnA.DataType.NumericScale)
                    AddException(new ColumnDifferentScaleException(databaseName, nomeTabelaB, columnB.Name, columnB.DataType.NumericScale, columnA.DataType.NumericScale));

                if ((columnB.DefaultConstraint != null && columnA.DefaultConstraint == null) || (columnB.DefaultConstraint == null && columnA.DefaultConstraint != null))
                    AddException(new ColumnDefaultValuePropertyDifferentException(databaseName, nomeTabelaA, columnA.Name, columnB.DefaultConstraint?.Text, columnA.DefaultConstraint?.Text));

                if (columnB.DefaultConstraint != null && columnA.DefaultConstraint != null && columnB.DefaultConstraint.Text != columnA.DefaultConstraint.Text)
                    AddException(new ColumnDefaultValuePropertyDifferentException(databaseName, nomeTabelaA, columnA.Name, columnB.DefaultConstraint?.Text, columnA.DefaultConstraint?.Text));
            }

           // Uniques(databaseName, tableA, tableB);
          //  Indices(databaseName, tableA, tableB);
           // ForeignKeys(databaseName, tableA, tableB);
          //  Triggers(databaseName, tableA, tableB);
        }

        private void AddException(MigrationFailedException exception) {
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

            throw new MigrationFailedException(stringBuilder.ToString());
        }
    }
}
