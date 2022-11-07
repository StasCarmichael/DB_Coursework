using System.Collections.Generic;

namespace Data.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            ReceiptDetails = new List<ReceiptDetail>();
        }


        public int ProductCategoryId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }


        // Navigation properties
        public virtual ProductCategory Category { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } 
    }
}
