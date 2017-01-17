# Test.Fluentmigrator 1.0.0

A simply tool to facility *TDD* in database migration using "FluentMigrator":https://github.com/schambers/fluentmigrator.

Usage
-------
First of all, create test for your migration before implement your migration (The first step of the TDD: https://en.wikipedia.org/wiki/Test-driven_development).

Create the script file of the database before the migration and then create a secod script file after the migration;

Create a script file in our test project before the migration to a file schema_0.sql.

```sql
CREATE TABLE [dbo].[Example](
	[Id] [bigint] NOT NULL
)
```

Create a script file in our test project after runed the migration to a file schema_1.sql. (i.e. The description column was added.)

```sql
CREATE TABLE [dbo].[Example](
	[Id] [bigint] NOT NULL,
	[Description] [nvarchar](250) NOT NULL
)
```

After this, create the test

```csharp
[Test]
public void ShouldMigrateToVersionOne() {
    var databaseTest = new DatabaseTest().ActualDatabase(Resources.schema_0)
                                         .ObjectiveDatabase(Resources.schema_1);

    databaseTest.RunMigration(1, "AssemblyNameProjectWithTheFluentMigration");

    Assert.DoesNotThrow(() => databaseTest.Compare());
}
```

After run, the test should break and finally you need to implement your migration.

Get it!
-------
You can clone and build Test.Fluentmigrator yourself, but for those of us who are happy with prebuilt binaries, there's [a NuGet package](https://www.nuget.org/packages/Test.Fluentmigrator/).
