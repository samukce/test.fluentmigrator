namespace Test.Fluentmigrator {
    public class DatabaseInfo {
        public string DatabaseName { get; set; }
        public string ServerHostname { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Collation { get; set; }

        public string GetConnectionString() {
            return $@"Server={ServerHostname};Database={DatabaseName};User ID={User};Password={Password};";
        }
    }
}