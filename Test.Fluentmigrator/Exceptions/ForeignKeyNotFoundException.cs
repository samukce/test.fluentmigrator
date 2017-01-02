namespace Test.Fluentmigrator.Exceptions {
    public class ForeignKeyNotFoundException : MigrationFailedException {
        public ForeignKeyNotFoundException(string databaseName, string tableName, string fkName)
            : base($"Foreign key {fkName} was not found in table {tableName} and database {databaseName}.") { }
    }
}
