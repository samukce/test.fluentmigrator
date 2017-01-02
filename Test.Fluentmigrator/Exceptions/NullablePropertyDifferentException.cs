namespace Test.Fluentmigrator.Exceptions {
    public class NullablePropertyDifferentException : MigrationFailedException {
        public NullablePropertyDifferentException(string databaseName, string tableName, string columnName, bool nullable)
            : base(string.Format("The {2} column in the {1} table in the {0} database should {3}be NULLABLE.", databaseName, tableName, columnName, nullable ? string.Empty : "NOT ")) { }
    }
}
