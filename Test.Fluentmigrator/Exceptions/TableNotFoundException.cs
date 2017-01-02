namespace Test.Fluentmigrator.Exceptions {
    public class TableNotFoundException : MigrationFailedException {
        public TableNotFoundException(string tableName, string databaseName)
            : base($"Table {tableName} was not found in database {databaseName}.") { }
    }
}
