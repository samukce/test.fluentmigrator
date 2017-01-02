using System;

namespace Test.Fluentmigrator.Exceptions {
    public class TestFluentMigratorException : Exception {
        public TestFluentMigratorException(string message) : base(message) {
        }
    }
}