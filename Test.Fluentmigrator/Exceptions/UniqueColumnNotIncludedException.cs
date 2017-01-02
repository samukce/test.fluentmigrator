namespace Test.Fluentmigrator.Exceptions {
    public class UniqueColumnNotIncludedException : MigrationFailedException {
        public UniqueColumnNotIncludedException(string databaseName, string tableName, string column, string unique)
            : base($"Column {column} not included in unique {unique} in table {tableName} and database {databaseName}.") { }
    }
}
