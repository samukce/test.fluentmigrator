namespace Test.Fluentmigrator.Exceptions {
    public class ColumnNotFoundException : MigrationFailedException {
        public ColumnNotFoundException(string databaseName, string tableName, string columnName)
            : base($"Column {columnName} was not found in table {tableName} and database {databaseName}.") { }
    }
}
