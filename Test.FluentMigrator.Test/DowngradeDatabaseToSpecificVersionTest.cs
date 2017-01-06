using NUnit.Framework;
using Test.Fluentmigrator;
using Test.Fluentmigrator.Exceptions;

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

        [Test]
        public void ShouldMigrateFromFourToVersionZeroWhenPutTag() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_2_4, Resources.script_data_test_migration_database_2_4)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RunMigration(0, "Test.FluentMigrator.Test", new[] { "DEV" });

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldNotMigrateFromFourToVersionZeroWhenNotPutTag() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_2_4, Resources.script_data_test_migration_database_2_4)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RunMigration(0, "Test.FluentMigrator.Test");

            Assert.Throws<MigrationFailedException>(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldRevertOneVersionAndShouldBeZeroVersion() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_1, Resources.script_data_test_migration_database_1_1)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RevertMigrationBefore(1, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldRevertTwoShouldBeOneVersion() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_1);

            databaseTest.RevertMigrationBefore(2, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }

        [Test]
        public void ShouldRevertTwoAndOneVersionAndShouldBeZeroVersion() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_1_0);

            databaseTest.RevertMigrationBefore(2, "Test.FluentMigrator.Test");
            databaseTest.RevertMigrationBefore(1, "Test.FluentMigrator.Test");

            Assert.DoesNotThrow(() => databaseTest.Compare());
        }
    }
}
