namespace Test.Fluentmigrator.Exceptions {
    public class IndexeNotFoundException : MigrationFailedException {
        public IndexeNotFoundException(string indixeName, string databaseName, string table)
            : base($"The {indixeName} Indexe was not found in the {databaseName} database in the {table} table.") { }
    }
}
