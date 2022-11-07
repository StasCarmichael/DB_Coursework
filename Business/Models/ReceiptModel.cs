using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class ReceiptModel : BaseModel
    {
        public ReceiptModel()
        {
            ReceiptDetailsIds = new List<int>();
        }


        public int CustomerId { get; set; }
        public DateTime OperationDate { get; set; }
        public bool IsCheckedOut { get; set; }


        // Navigation property
        public virtual ICollection<int> ReceiptDetailsIds { get; set; }
    }
}
