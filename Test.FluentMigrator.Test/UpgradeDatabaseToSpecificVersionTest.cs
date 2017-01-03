using NUnit.Framework;
using Test.Fluentmigrator;
using Test.Fluentmigrator.Exceptions;

namespace Test.FluentMigrator.Test {
    [TestFixture]
    public class UpgradeDatabaseToSpecificVersionTest {
        [Test]
        public void ShouldMigrateToVersionOne() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_0)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_1);

            databaseTest.RunMigration(1, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromOneToVersionTwo() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_1, Resources.script_data_test_migration_database_1_1)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_2);

            databaseTest.RunMigration(2, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromZeroToVersionTwo() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_0)
                                                .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_2);

            databaseTest.RunMigration(2, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromTwoToVersionThree() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_2_3);

            databaseTest.RunMigration(3, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldMigrateFromZeroToVersionFourWhenPutTag() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_0)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_2_4);

            databaseTest.RunMigration(4, "Test.FluentMigrator.Test", new[] { "DEV" });

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldNotMigrateFromZeroToVersionFourWhenNotPutTag() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_0)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_2_4);

            databaseTest.RunMigration(4, "Test.FluentMigrator.Test");

            Assert.Throws<MigrationFailedException>(() => databaseTest.Compare());
        }
    }
}
