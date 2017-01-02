namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDifferentScaleException : MigrationFailedException {
        public ColumnDifferentScaleException(string database, string table, string column, int objective, int actual)
            : base(string.Format("The {2} Column in the {1} table in the {0} database should be {3} but is {4} of scale.", database, table, column, objective, actual)) { }
    }
}
