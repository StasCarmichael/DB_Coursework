using System.Collections.Generic;

namespace Business.Models
{
    public class ProductCategoryModel : BaseModel
    {
        public ProductCategoryModel()
        {
            ProductIds = new List<int>();
        }

        public string CategoryName { get; set; }


        // Navigation properties
        public virtual ICollection<int> ProductIds { get; set; }
    }
}
