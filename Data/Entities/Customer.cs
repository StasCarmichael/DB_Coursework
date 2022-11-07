using System.Collections.Generic;

namespace Data.Entities
{
    public class Customer : BaseEntity
    {
        public Customer()
        {
            Receipts = new List<Receipt>();
        }


        public int PersonId { get; set; }
        public int DiscountValue { get; set; }


        // Navigation properties
        public virtual Person Person { get; set; }
        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
