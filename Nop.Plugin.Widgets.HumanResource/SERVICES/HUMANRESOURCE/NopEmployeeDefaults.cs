using Nop.Core.Caching;
using $ucprojectname$.Core.Domains.$ucprojectname$;

namespace $ucprojectname$.Services.$ucprojectname$
{
    /// <summary>
    /// Represents default values related to hr services
    /// </summary>
    public static partial class Nop$Entity$Defaults
    {

        #region Caching defaults

        #region $Entity$s

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent $lcent$ ID
        /// {1} : show hidden records?
        /// {2} : current customer ID
        /// {3} : store ID
        /// </remarks>
        public static CacheKey $Entity$sByParent$Entity$CacheKey => new("Nop.$lcent$.byparent.{0}-{1}-{2}-{3}", $Entity$sByParent$Entity$Prefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {0} : parent $lcent$ ID
        /// </remarks>
        public static string $Entity$sByParent$Entity$Prefix => "Nop.$lcent$.byparent.{0}";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : parent $lcent$ id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// {3} : show hidden records?
        /// </remarks>
        public static CacheKey $Entity$sChildIdsCacheKey => new("Nop.$lcent$.childids.{0}-{1}-{2}-{3}", $Entity$sChildIdsPrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {0} : parent $lcent$ ID
        /// </remarks>
        public static string $Entity$sChildIdsPrefix => "Nop.$lcent$.childids.{0}";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey $Entity$sHomepageCacheKey => new("Nop.$lcent$.homepage.", $Entity$sHomepagePrefix);

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// {1} : roles of the current user
        /// </remarks>
        public static CacheKey $Entity$sHomepageWithoutHiddenCacheKey => new("Nop.$lcent$.homepage.withouthidden-{0}-{1}", $Entity$sHomepagePrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string $Entity$sHomepagePrefix => "Nop.$lcent$.homepage.";

        /// <summary>
        /// Key for caching of $lcent$ breadcrumb
        /// </summary>
        /// <remarks>
        /// {0} : $lcent$ id
        /// {1} : roles of the current user
        /// {2} : current store ID
        /// {3} : language ID
        /// </remarks>
        public static CacheKey $Entity$BreadcrumbCacheKey => new("Nop.$lcent$.breadcrumb.{0}-{1}-{2}-{3}", $Entity$BreadcrumbPrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string $Entity$BreadcrumbPrefix => "Nop.$lcent$.breadcrumb.";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : current store ID
        /// {1} : roles of the current user
        /// {2} : show hidden records?
        /// </remarks>
        public static CacheKey $Entity$sAllCacheKey => new("Nop.$lcent$.all.{0}-{1}-{2}", NopEntityCacheDefaults<$Entity$>.AllPrefix);

        #endregion

        #region Products

        /// <summary>
        /// Key for "related" product displayed on the product details page
        /// </summary>
        /// <remarks>
        /// {0} : current product id
        /// {1} : show hidden records?
        /// </remarks>
        public static CacheKey RelatedProductsCacheKey => new("Nop.relatedproduct.byproduct.{0}-{1}", RelatedProductsPrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        public static string RelatedProductsPrefix => "Nop.relatedproduct.byproduct.{0}";

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        public static CacheKey ProductsHomepageCacheKey => new("Nop.product.homepage.");

        /// <summary>
        /// Key for caching identifiers of $lcent$ featured products
        /// </summary>
        /// <remarks>
        /// {0} : $lcent$ id
        /// {1} : customer role Ids
        /// {2} : current store ID
        /// </remarks>
        public static CacheKey $Entity$FeaturedProductsIdsKey => new("Nop.product.featured.by$lcent$.{0}-{1}-{2}", $Entity$FeaturedProductsIdsPrefix, FeaturedProductIdsPrefix);
        public static string $Entity$FeaturedProductsIdsPrefix => "Nop.product.featured.by$lcent$.{0}";

        public static string FeaturedProductIdsPrefix => "Nop.product.featured.";
        #endregion

        #region Product attributes

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        public static CacheKey ProductAttributeMappingsByProductCacheKey => new("Nop.productattributemapping.byproduct.{0}");

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product attribute mapping ID
        /// </remarks>
        public static CacheKey ProductAttributeValuesByAttributeCacheKey => new("Nop.productattributevalue.byattribute.{0}");

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        public static CacheKey ProductAttributeCombinationsByProductCacheKey => new("Nop.productattributecombination.byproduct.{0}");

        /// <summary>
        /// Gets a key for caching
        /// </summary>
        /// <remarks>
        /// {0} : Product attribute ID
        /// </remarks>
        public static CacheKey PredefinedProductAttributeValuesByAttributeCacheKey => new("Nop.predefinedproductattributevalue.byattribute.{0}");

        #endregion

        #region Specification attributes

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {0} : product ID
        /// </remarks>
        public static string ProductSpecificationAttributeByProductPrefix => "Nop.productspecificationattribute.byproduct.{0}";

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        /// <remarks>
        /// {1} (not 0, see the <ref>ProductSpecificationAttributeAllByProductIdCacheKey</ref>) :specification attribute option ID
        /// </remarks>
        public static string ProductSpecificationAttributeAllByProductPrefix => "Nop.productspecificationattribute.byproduct.";

        /// <summary>
        /// Key for specification attributes caching (product details page)
        /// </summary>
        public static CacheKey SpecificationAttributesWithOptionsCacheKey => new("Nop.specificationattribute.withoptions.");

        /// <summary>
        /// Key for specification attribute options by $lcent$ ID caching
        /// </summary>
        /// <remarks>
        /// {0} : $lcent$ ID
        /// </remarks>
        public static CacheKey SpecificationAttributeOptionsBy$Entity$CacheKey => new("Nop.specificationattributeoption.by$lcent$.{0}", FilterableSpecificationAttributeOptionsPrefix);

        /// <summary>
        /// Gets a key pattern to clear cache
        /// </summary>
        public static string FilterableSpecificationAttributeOptionsPrefix => "Nop.filterablespecificationattributeoptions";

        #endregion

        #endregion
    }
}