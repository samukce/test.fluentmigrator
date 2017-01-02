namespace Test.Fluentmigrator.Exceptions {
    public class UniqueNotFoundException : MigrationFailedException {
        public UniqueNotFoundException(string databaseName, string tableName, string unique)
            : base($"The {unique} Unique was not found in the {tableName} table in the {databaseName} database.") { }
    }
}
