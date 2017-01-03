using FluentMigrator;

namespace Test.FluentMigrator.Test.Test.Migrations {
    [Migration(1)]
    public class MigrationTestVersion1 : Migration {
        public override void Up() {
            Delete.UniqueConstraint("UQ_TABLE_column_01")
                  .FromTable("TABLE");

            Alter.Column("column01")
                 .OnTable("TABLE")
                 .AsString(50)
                 .Unique("UQ_TABLE_column_01");

            Alter.Column("column02")
                 .OnTable("TABLE")
                 .AsInt32()
                 .Nullable();

            Rename.Column("columnInt")
                  .OnTable("TABLE")
                  .To("column_integer");

            Delete.Column("column_date")
                  .FromTable("TABLE");

            Create.Table("DATES")
                  .WithColumn("id")
                  .AsInt64()
                  .PrimaryKey()
                  .Identity()
                  .WithColumn("column")
                  .AsDateTime()
                  .NotNullable()
                  .WithColumn("id_TABLE")
                  .AsInt64();

            Create.ForeignKey("FK_DATES_id_TABLE")
                  .FromTable("DATES")
                  .ForeignColumn("id_TABLE")
                  .ToTable("TABLE")
                  .PrimaryColumn("id");
        }

        public override void Down() {
            Delete.UniqueConstraint("UQ_TABLE_column_01")
                  .FromTable("TABLE");
            
            Alter.Column("column01")
                 .OnTable("TABLE")
                 .AsString(255)
                 .Unique("UQ_TABLE_column_01");

            Alter.Column("column02")
                 .OnTable("TABLE")
                 .AsString(255)
                 .Nullable();
            
            Rename.Column("column_integer")
                  .OnTable("TABLE")
                  .To("columnInt");

            Create.Column("column_date")
                  .OnTable("TABLE")
                  .AsDateTime()
                  .Nullable();

            Delete.ForeignKey("FK_DATES_id_TABLE")
                  .OnTable("DATES");

            Delete.Table("DATES");
        }
    }
}
