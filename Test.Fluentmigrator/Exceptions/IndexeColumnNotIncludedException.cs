namespace Test.Fluentmigrator.Exceptions {
    public class IndexeColumnNotIncludedException : MigrationFailedException {
        public IndexeColumnNotIncludedException(string databaseName, string tableName, string column, string indexe)
            : base($"Column {column} not included in indexe {indexe} in table {tableName} and database {databaseName}.") { }
    }
}
