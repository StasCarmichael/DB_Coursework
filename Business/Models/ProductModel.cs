using System.Collections.Generic;

namespace Business.Models
{
    public class ProductModel : BaseModel
    {
        public ProductModel()
        {
            ReceiptDetailIds = new List<int>();
        }


        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }


        // Navigation properties
        public virtual ICollection<int> ReceiptDetailIds { get; set; }
    }
}
