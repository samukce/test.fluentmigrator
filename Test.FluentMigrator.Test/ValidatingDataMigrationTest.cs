using NUnit.Framework;
using Test.Fluentmigrator;

namespace Test.FluentMigrator.Test {
    [TestFixture]
    public class ValidatingDataMigrationTest {
        [Test]
        public void ShouldMigrateToVersionThree() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_test_migration_database_1_2, Resources.script_data_test_migration_database_1_2)
                                                 .ObjectiveDatabase(Resources.script_schema_test_migration_database_2_3);

            databaseTest.RunMigration(3, "Test.FluentMigrator.Test");

            databaseTest.ExecuteCommandActualDatabase(
                    c => {
                        c.CommandText = "select column01 from [TABLE] ";
                        using (var dataReader = c.ExecuteReader()) {
                            dataReader.Read();
                            Assert.AreEqual("Done!", dataReader.GetString(0));
                        }
                    });
        }
    }
}
