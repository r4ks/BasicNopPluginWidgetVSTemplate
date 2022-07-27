using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Data.Mapping.Builders.$ucprojectname$
{
    /// <summary>
    /// Represents a $lcent$ entity builder
    /// </summary>
    public partial class $Entity$Builder : NopEntityBuilder<$Entity$>
    {
        #region Methods

        /// <summary>
        /// Apply entity configuration
        /// </summary>
        /// <param name="table">Create table expression builder</param>
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof($Entity$.Name)).AsString(400).NotNullable()
                .WithColumn(nameof($Entity$.PageSizeOptions)).AsString(200).Nullable();
        }

        #endregion
    }
}