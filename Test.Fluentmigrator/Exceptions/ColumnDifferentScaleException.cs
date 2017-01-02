namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDifferentScaleException : MigrationFailedException {
        public ColumnDifferentScaleException(string database, string table, string column, int objective, int actual)
            : base(string.Format("Column {2} in the table {1} in the database {0} should be {3} but is {4} of scale.", database, table, column, objective, actual)) { }
    }
}
