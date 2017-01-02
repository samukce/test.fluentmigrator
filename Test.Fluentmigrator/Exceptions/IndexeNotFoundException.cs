namespace Test.Fluentmigrator.Exceptions {
    public class IndexeNotFoundException : MigrationFailedException {
        public IndexeNotFoundException(string indixeName, string databaseName, string table)
            : base($"Indexe {indixeName} was not found in database {databaseName} in the table {table}.") { }
    }
}
