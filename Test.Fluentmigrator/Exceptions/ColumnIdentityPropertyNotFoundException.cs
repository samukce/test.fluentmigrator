namespace Test.Fluentmigrator.Exceptions {
    public class ColumnIdentityPropertyNotFoundException : MigrationFailedException {
        public ColumnIdentityPropertyNotFoundException(string databaseName, string tableName, string columnName, bool identity)
            : base(string.Format("The {2} Column in the {1} table in the {0} database should {3}be IDENTITY.", databaseName, tableName, columnName, identity ? string.Empty : "NOT ")) { }
    }
}
