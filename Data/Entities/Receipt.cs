using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public class Receipt : BaseEntity
    {
        public Receipt()
        {
            ReceiptDetails = new List<ReceiptDetail>();
        }


        public int CustomerId { get; set; }
        public DateTime OperationDate { get; set; }
        public bool IsCheckedOut { get; set; }


        // Navigation property
        public virtual Customer Customer { get; set; }
        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}
