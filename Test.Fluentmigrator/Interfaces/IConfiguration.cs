namespace Test.Fluentmigrator.Interfaces {
    public interface IConfiguration {
        void LoadInit(DatabaseInfo databaseInfo, string schemaScript, string dataScript = null);
    }
}