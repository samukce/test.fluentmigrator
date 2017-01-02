namespace Test.Fluentmigrator.Exceptions {
    public class ForeignKeyNotFoundException : MigrationFailedException {
        public ForeignKeyNotFoundException(string databaseName, string tableName, string fkName)
            : base($"The {fkName} Foreign key was not found in the {tableName} table in the {databaseName} database.") { }
    }
}
