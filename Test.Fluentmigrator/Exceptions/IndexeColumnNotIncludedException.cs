namespace Test.Fluentmigrator.Exceptions {
    public class IndexeColumnNotIncludedException : MigrationFailedException {
        public IndexeColumnNotIncludedException(string databaseName, string tableName, string column, string indexe)
            : base($"The {column} Column not included in the {indexe} indexe in the {tableName} table in the {databaseName} database.") { }
    }
}
