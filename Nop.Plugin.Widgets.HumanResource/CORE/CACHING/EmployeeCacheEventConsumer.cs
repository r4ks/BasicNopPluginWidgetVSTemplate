using System.Threading.Tasks;
using $ucprojectname$.Core.Domains.$ucprojectname$;
using $ucprojectname$.Services.$ucprojectname$;
using Nop.Services.Caching;

namespace $ucprojectname$.Core.Caching
{
    /// <summary>
    /// Represents a $lcent$ cache event consumer
    /// </summary>
    public partial class $Entity$CacheEventConsumer : CacheEventConsumer<$Entity$>
    {
        /// <summary>
        /// Clear cache data
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <param name="entityEventType">Entity event type</param>
        /// <returns>A task that represents the asynchronous operation</returns>
        protected override async Task ClearCacheAsync($Entity$ entity, EntityEventType entityEventType)
        {
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sByParent$Entity$Prefix, entity);
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sByParent$Entity$Prefix, entity.Parent$Entity$Id);
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sChildIdsPrefix, entity);
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sChildIdsPrefix, entity.Parent$Entity$Id);
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$sHomepagePrefix);
            await RemoveByPrefixAsync(Nop$Entity$Defaults.$Entity$BreadcrumbPrefix);

            await base.ClearCacheAsync(entity, entityEventType);
        }
    }
}
