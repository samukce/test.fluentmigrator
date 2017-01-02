namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDifferentTypeException : MigrationFailedException {
        public ColumnDifferentTypeException(string database, string table, string column, string objective, string actual)
            : base(string.Format("Column {2} in the table {1} in the database {0} should be {3} but is {4}.", database, table, column, objective, actual)) { }
    }
}
