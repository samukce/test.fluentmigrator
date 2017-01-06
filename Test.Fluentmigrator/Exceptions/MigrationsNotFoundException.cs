namespace Test.Fluentmigrator.Exceptions {
    public class MigrationsNotFoundException : MigrationFailedException {
        public MigrationsNotFoundException() : base("Migrations not found") {
        }
    }
}
