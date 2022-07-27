using Nop.Core.Caching;

namespace $ucprojectname$.Web.Infrastructure.Cache
{
    public static partial class NopModelCacheDefaults
    {
        /// <summary>
        /// Key for nopCommerce.com news cache
        /// </summary>
        public static CacheKey OfficialNewsModelKey => new("Nop.pres.admin.official.news");
        
        /// <summary>
        /// Key for $lcent$s caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        public static CacheKey $Entity$sListKey => new("Nop.pres.admin.$lcent$s.list-{0}", $Entity$sListPrefixCacheKey);
        public static string $Entity$sListPrefixCacheKey => "Nop.pres.admin.$lcent$s.list";

    }
}
