using System.Text;

namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDefaultValuePropertyDifferentException : MigrationFailedException {
        public ColumnDefaultValuePropertyDifferentException(string databaseName, string tableName, string columnName, string actual, string objective)
            : base(GetMessage(databaseName, tableName, columnName, actual, objective)) { }

        private static string GetMessage(string database, string tableName, string column,
                                         string actual, string objective) {
            var message = new StringBuilder(string.Format("Column {2} in the table {1} in the database {0} ", database, tableName, column));

            if (objective == null && actual != null) {
                message.Append($"should not have default value but is {actual}.");
            } else if (objective != null && actual == null) {
                message.Append($"should have default value {objective} but not was setted.");
            } else if (objective != null) {
                message.Append($"should have default value {objective} but was setted {actual}.");
            }

            return message.ToString();
        }
    }
}
