namespace Test.Fluentmigrator.Exceptions {
    public class MigrationFailedException : TestFluentMigratorException {
        public MigrationFailedException(string message) : base(message) {
        }
    }
}