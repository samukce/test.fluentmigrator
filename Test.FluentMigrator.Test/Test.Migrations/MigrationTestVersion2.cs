using FluentMigrator;

namespace Test.FluentMigrator.Test.Test.Migrations {
    [Migration(2)]
    public class MigrationTestVersion2 : Migration {
        public override void Up() {
            Create.Table("TABLE2")
                  .WithColumn("id")
                    .AsInt64()
                    .PrimaryKey()
                    .Identity()
                  .WithColumn("column")
                    .AsString();
        }

        public override void Down() {
            Delete.Table("TABLE2");
        }
    }
}
