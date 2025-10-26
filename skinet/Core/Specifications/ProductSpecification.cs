using Core.Entities;

namespace Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams specParams)
            : base(x =>
                (specParams.Brands.Count==0 || specParams.Brands.Contains(x.Brand)) &&
                specParams.Types.Count==0 || specParams.Types.Contains(x.Type)
            )
        {
            //

            switch (specParams.Sort)
            {
                case "priceAsc":
                    this.AddOrderBy(x => x.Price);
                    break;
                case "priceDesc":
                    this.AddOrderByDescending(x => x.Price);
                    break;
                default:
                    this.AddOrderBy(x => x.Name);
                    break;
            }
        }
    }
}
