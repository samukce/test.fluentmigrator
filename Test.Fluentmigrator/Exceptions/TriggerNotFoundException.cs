namespace Test.Fluentmigrator.Exceptions {
    public class TriggerNotFoundException : MigrationFailedException {
        public TriggerNotFoundException(string databaseName, string tableName, string triggerName, string triggerContent)
            : base($"The {triggerName} Trigger was not found in the {tableName} table in the {databaseName} database. Content: {triggerContent}") { }
    }
}
