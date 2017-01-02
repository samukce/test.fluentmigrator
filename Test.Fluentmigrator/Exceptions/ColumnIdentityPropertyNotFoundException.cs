namespace Test.Fluentmigrator.Exceptions {
    public class ColumnIdentityPropertyNotFoundException : MigrationFailedException {
        public ColumnIdentityPropertyNotFoundException(string databaseName, string tableName, string columnName, bool identity)
            : base(string.Format("Column {2} in the table {1} in the database {0} should {3}be IDENTITY.", databaseName, tableName, columnName, identity ? string.Empty : "NOT ")) { }
    }
}
