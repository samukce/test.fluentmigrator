using NUnit.Framework;
using Test.Fluentmigrator;

namespace Test.FluentMigrator.Test {
    [TestFixture]
    public class DowngradeDatabaseToSpecificVersionTest {

        [Test]
        public void ShouldMigrateFromTwoToVersionOne() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_1);

            databaseTest.RunMigration(1, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromOneToVersionZero() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_1, Resources.script_data_test_migration_database_1_1)
                                                .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RunMigration(0, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromTwoToVersionZero() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RunMigration(0, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }
    }
}
