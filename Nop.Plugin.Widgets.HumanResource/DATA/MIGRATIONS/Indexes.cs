using FluentMigrator;
using FluentMigrator.SqlServer;
using Nop.Data.Migrations;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Data.Migrations
{
    [NopMigration("2020/03/13 09:36:08:9037677", "Nop.Data base indexes", MigrationProcessType.Installation)]
    public class Indexes : AutoReversingMigration
    {
        #region Methods

        public override void Up()
        {

            Create.Index("IX_$Entity$_Parent$Entity$Id").OnTable(nameof($Entity$))
                .OnColumn(nameof($Entity$.Parent$Entity$Id)).Ascending()
                .WithOptions().NonClustered();

            Create.Index("IX_$Entity$_LimitedToStores").OnTable(nameof($Entity$))
                .OnColumn(nameof($Entity$.LimitedToStores)).Ascending()
                .WithOptions().NonClustered();

            Create.Index("IX_$Entity$_DisplayOrder").OnTable(nameof($Entity$))
                .OnColumn(nameof($Entity$.DisplayOrder)).Ascending()
                .WithOptions().NonClustered();

            Create.Index("IX_$Entity$_Deleted_Extended").OnTable(nameof($Entity$))
                .OnColumn(nameof($Entity$.Deleted)).Ascending()
                .WithOptions().NonClustered()
                .Include(nameof($Entity$.Id))
                .Include(nameof($Entity$.Name))
                .Include(nameof($Entity$.SubjectToAcl)).Include(nameof($Entity$.LimitedToStores))
                .Include(nameof($Entity$.Published));

            Create.Index("IX_$Entity$_SubjectToAcl").OnTable(nameof($Entity$))
                .OnColumn(nameof($Entity$.SubjectToAcl)).Ascending()
                .WithOptions().NonClustered();

        }

        #endregion
    }
}
