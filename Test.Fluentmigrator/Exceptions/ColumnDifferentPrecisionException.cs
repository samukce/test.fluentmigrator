namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDifferentPrecisionException : MigrationFailedException {
        public ColumnDifferentPrecisionException(string database, string table, string column, int objective, int actual)
            : base(string.Format("The {2} Column in the {1} table in the {0} database should be {3} but is {4} of precision.", database, table, column, objective, actual)) { }
    }
}
