using System.Collections.Generic;

namespace Data.Entities
{
    public class ProductCategory : BaseEntity
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }


        public string CategoryName { get; set; }


        // Navigation properties
        public virtual ICollection<Product> Products { get; set; }
    }
}
