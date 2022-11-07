using System;
using System.Collections.Generic;

namespace Business.Models
{
    public class CustomerModel : BaseModel
    {
        public CustomerModel()
        {
            ReceiptsIds = new List<int>();
        }


        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public int DiscountValue { get; set; }


        // Navigation properties
        public virtual ICollection<int> ReceiptsIds { get; set; }
    }
}
