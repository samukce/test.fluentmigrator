namespace Test.Fluentmigrator.Exceptions {
    public class UniqueColumnNotIncludedException : MigrationFailedException {
        public UniqueColumnNotIncludedException(string databaseName, string tableName, string column, string unique)
            : base($"The {column} Column not included in the {unique} unique in the {tableName} table in the {databaseName} database.") { }
    }
}
