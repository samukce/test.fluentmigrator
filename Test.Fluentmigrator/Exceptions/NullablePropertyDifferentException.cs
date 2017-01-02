namespace Test.Fluentmigrator.Exceptions {
    public class NullablePropertyDifferentException : MigrationFailedException {
        public NullablePropertyDifferentException(string databaseName, string tableName, string columnName, bool nullable)
            : base(string.Format("The column {2} in table {1} in database {0} should {3}be NULLABLE.", databaseName, tableName, columnName, nullable ? string.Empty : "NOT ")) { }
    }
}
