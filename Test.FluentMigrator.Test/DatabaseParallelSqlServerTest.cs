using NUnit.Framework;
using Test.Fluentmigrator;
using Test.Fluentmigrator.Exceptions;

namespace Test.FluentMigrator.Test {
    [TestFixture]
    public class DatabaseParallelSqlServerTest {
        [Test]
        public void ShouldThrowTableNotFoundExceptionIfTableInDatabaseBNotExistInDatabaseA() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_table_missing_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_table_missing_database_b);

            Assert.Throws<TableNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowTableNotFoundExceptionIfTableInDatabaseBNotExistInDatabaseAInTheSameSchema() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_table_missing_database_a_diferent_schema)
                                                 .ObjectiveDatabase(Resources.script_schema_table_missing_database_b_diferent_schema);

            Assert.Throws<TableNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowColumnNotFoundExceptionIfColumnNotExistInTheSameTable() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_missing_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_missing_database_b);

            Assert.Throws<ColumnNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowNullablePropertyExceptionIfNullablePropertyDifferentBetweenTheSameColumn() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_nullable_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_nullable_database_b);

            Assert.Throws<NullablePropertyDifferentException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowUniqueNotExistIfUniqueNotExistToTheSameColumn() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_unique_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_unique_database_b);

            Assert.Throws<UniqueNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowUniqueColumnNotIncludedException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_not_included_unique_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_not_included_unique_database_b);

            Assert.Throws<UniqueColumnNotIncludedException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnDifferentTypeException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_different_type_database_a)
                                                .ObjectiveDatabase(Resources.script_schema_column_different_type_database_b);

            Assert.Throws<ColumnDifferentTypeException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnDifferentSizeException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_different_size_database_a)
                                                .ObjectiveDatabase(Resources.script_schema_column_different_size_database_b);

            Assert.Throws<ColumnDifferentSizeException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnDifferentPrecisionException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_different_precision_database_a)
                                               .ObjectiveDatabase(Resources.script_schema_column_different_precision_database_b);

            Assert.Throws<ColumnDifferentPrecisionException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnDifferentScaleException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_scale_notequal_database_a)
                                               .ObjectiveDatabase(Resources.script_schema_column_scale_notequal_database_b);

            Assert.Throws<ColumnDifferentScaleException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldValidateTableMissingBidirectional() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_table_missing_database_tocompare_a)
                                                 .ObjectiveDatabase(Resources.script_schema_table_missing_database_tocompare_b);

            Assert.Throws<TableNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldValidateColumnMissingBidirectional() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_missing_datababse_tocompare_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_missing_datababse_tocompare_b);

            Assert.Throws<ColumnNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowIndexeNotFoundException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_indexe_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_indexe_database_b);

            Assert.Throws<IndexeNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowIndexeColumnNotIncludedException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_not_included_indexe_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_not_included_indexe_database_b);

            Assert.Throws<IndexeColumnNotIncludedException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnIdentityPropertyNotFoundException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_not_found_identity_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_not_found_identity_database_b);

            Assert.Throws<ColumnIdentityPropertyNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowColumnDefaultValuePropertyDifferentException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_column_value_default_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_column_value_default_database_b);

            Assert.Throws<ColumnDefaultValuePropertyDifferentException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldTthrowForeignKeyNotFoundException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_foreign_key_database_a)
                                                 .ObjectiveDatabase(Resources.script_schema_foreign_key_database_b);

            Assert.Throws<ForeignKeyNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowTriggerNotFoundException() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_trigger_database_a)
                                                .ObjectiveDatabase(Resources.script_schema_trigger_database_b);

            Assert.Throws<TriggerNotFoundException>(() => databaseTest.Compare(false));
        }

        [Test]
        public void ShouldThrowAllExceptions() {
            var databaseTest = new DatabaseTest().ActualDatabase(Resources.script_schema_table_and_column_missing_a)
                                                .ObjectiveDatabase(Resources.script_schema_table_and_column_missing_b);

            try {
                databaseTest.Compare();
                Assert.Fail();
            } catch (MigrationFailedException ex) {
                Assert.AreEqual("Column campo01 was not found in table dbo.TABLE2 in the database TEST_DATABASE.\r\n" +
                                "Table dbo.TABLE1 was not found in the database TEST_DATABASE.\r\n",
                                ex.Message);
            }
        }
    }
}
