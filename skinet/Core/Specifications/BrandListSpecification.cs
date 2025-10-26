using Core.Entities;

namespace Core.Specifications
{
    public class BrandListSpecification : BaseSpecification<Product,String>
    {
        public BrandListSpecification()
        {
            AddSelect(x => x.Brand);
            ApplyDistinct();
        }
    }
}
