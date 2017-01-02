namespace Test.Fluentmigrator.Exceptions {
    public class ColumnNotFoundException : MigrationFailedException {
        public ColumnNotFoundException(string databaseName, string tableName, string columnName)
            : base($"The {columnName} Column was not found in the {tableName} table in the {databaseName} database.") { }
    }
}
