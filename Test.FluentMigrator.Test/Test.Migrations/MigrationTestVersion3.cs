﻿using FluentMigrator;

namespace Test.FluentMigrator.Test.Test.Migrations {
    [Migration(3)]
    public class MigrationTestVersion3 : Migration {
        public override void Up() {
            Create.Table("TABLE3")
                  .WithColumn("file")
                  .AsBinary(2147483647)
                  .Nullable();

            Update.Table("TABLE").Set(new { column01 = "Done!" }).AllRows();
        }

        public override void Down() {
            Delete.Table("TABLE3");

            Update.Table("TABLE").Set(new { column01 = "Content before migration" }).AllRows();
        }
    }
}
