using FluentMigrator;

namespace Test.FluentMigrator.Test.Test.Migrations {
    [Tags("DEV")]
    [Migration(4)]
    public class MigrationTestVersion4 : Migration {
        public override void Up() {
            Create.Table("TABLE_BY_TAG")
                  .WithColumn("id")
                  .AsInt64()
                  .NotNullable()
                  .Identity();
        }

        public override void Down() {
            Delete.Table("TABLE_BY_TAG");
        }
    }
}
