namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDifferentTypeException : MigrationFailedException {
        public ColumnDifferentTypeException(string database, string table, string column, string objective, string actual)
            : base(string.Format("The {2} Column in the {1} table in the {0} database should be {3} but is {4}.", database, table, column, objective, actual)) { }
    }
}
