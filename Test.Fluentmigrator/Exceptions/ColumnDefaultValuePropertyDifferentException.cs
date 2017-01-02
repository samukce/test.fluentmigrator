using System.Text;

namespace Test.Fluentmigrator.Exceptions {
    public class ColumnDefaultValuePropertyDifferentException : MigrationFailedException {
        public ColumnDefaultValuePropertyDifferentException(string databaseName, string tableName, string columnName, string objective, string actual)
            : base(GetMessage(databaseName, tableName, columnName, objective, actual)) { }

        private static string GetMessage(string database, string tableName, string column,
                                         string objective, string actual) {
            var message = new StringBuilder(string.Format("The {2} Column in the {1} table in the {0} database ", database, tableName, column));

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
