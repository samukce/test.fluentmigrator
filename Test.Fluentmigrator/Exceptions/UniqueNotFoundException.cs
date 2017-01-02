namespace Test.Fluentmigrator.Exceptions {
    public class UniqueNotFoundException : MigrationFailedException {
        public UniqueNotFoundException(string databaseName, string tableName, string unique)
            : base($"Unique {unique} was not found in table {tableName} and database {databaseName}.") { }
    }
}
