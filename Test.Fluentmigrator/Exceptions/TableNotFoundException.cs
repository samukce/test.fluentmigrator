namespace Test.Fluentmigrator.Exceptions {
    public class TableNotFoundException : MigrationFailedException {
        public TableNotFoundException(string tableName, string databaseName)
            : base($"The {tableName} table was not found in the {databaseName} database.") { }
    }
}
