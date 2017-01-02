namespace Test.Fluentmigrator.Exceptions {
    public class TriggerNotFoundException : MigrationFailedException {
        public TriggerNotFoundException(string databaseName, string tableName, string triggerName, string triggerContent)
            : base($"Trigger {triggerName} was not found in table {tableName} in the database {databaseName}. Content: {triggerContent}") { }
    }
}
