namespace Test.Fluentmigrator.Interfaces {
    public interface IParallel {
        void Perform(DatabaseInfo actual, DatabaseInfo objective, bool allErrors);
    }
}
